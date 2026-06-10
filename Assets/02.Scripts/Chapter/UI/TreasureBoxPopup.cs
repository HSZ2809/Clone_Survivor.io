using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Zenject;

namespace ZUN
{
    /// <summary>
    /// Lottery popup controller.
    ///
    /// Slot layout (connect in Inspector in this clockwise order):
    ///  [ 0][ 1][ 2][ 3][ 4]   top    (left → right)
    ///  [15]            [ 5]
    ///  [14]            [ 6]   sides
    ///  [13]            [ 7]
    ///  [12][11][10][ 9][ 8]   bottom (right → left)
    ///
    /// Usage:
    ///   popup.Open(winners, candidatePool, onComplete);
    ///   popup.Close();
    ///
    /// Time.timeScale is NOT restored here — the caller (ChapterCtrl) is responsible.
    ///
    /// Winner count behaviour:
    ///   1-skill  → Phase A → B (1 light) → C (decelerate) → single center result
    ///   3-skill  → Phase A (3 lights) → single light reveal → 3 results
    ///   5-skill  → Fill/drain → Flash → Build-up → Train marquee → 5 sequential results
    /// </summary>
    public class TreasureBoxPopup : MonoBehaviour
    {
        [Inject] IManager_Audio manager_Audio;

        [Header("Slots (0-15, clockwise)")]
        [SerializeField] TrainSlot[] slots;

        [Header("Panel / Popup")]
        [SerializeField] CanvasGroup cg_dimPanel;
        [SerializeField] CanvasGroup cg_popup;
        [SerializeField] RectTransform rt_popup;

        [Header("Continue Button")]
        [SerializeField] Button btn_continue;
        [SerializeField] CanvasGroup cg_continue;
        [SerializeField] RectTransform rt_continue;

        [Header("Skip Hint")]
        [SerializeField] GameObject go_skipHint;

        [Header("Center Result (1-skill)")]
        [SerializeField] GameObject go_centerResult;
        [SerializeField] LotteryResult centerLotteryResult;
        [SerializeField] CanvasGroup cg_centerResult;
        [SerializeField] RectTransform rt_centerResult;

        [Header("Multi Winner Results (3-skill / 5-skill)  [0]=1st top → [4]=5th bot")]
        [SerializeField] GameObject go_multiResultPanel;
        [SerializeField] LotteryResult[] multiLotteryResults;
        [SerializeField] CanvasGroup[] cg_multiResults;
        [SerializeField] RectTransform[] rt_multiResults;

        [Header("Marquee Parameters")]
        [SerializeField] float minDelay = 0.03f;
        [SerializeField] float maxDelay = 0.22f;
        [SerializeField] AnimationCurve decelerationCurve;

        [Header("5-Skill Special Effect")]
        [SerializeField] AudioSource sfxSource;
        [SerializeField] AudioClip trainClip;

        const int   SlotCount      = 16;
        const float AppearDuration = 0.35f;
        const float FillDrainDelay = 0.08f;
        const float BuildUpOnTime  = 0.30f;
        const float BuildUpGapTime = 0.12f;

        Action _onComplete;
        Skill[] _winners;
        int[] _winnerLevels;
        int[] _winnerSlots;
        int _currentHead;
        bool _waitingForSkip;
        bool _skipRequested;
        bool _continuePressed;

        // ── lifecycle ────────────────────────────────────────

        void Awake()
        {
            if (decelerationCurve == null || decelerationCurve.keys.Length == 0)
                decelerationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        }

        void Update()
        {
            if (!_waitingForSkip) return;

            bool tapped = (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
                       || (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame);

            if (tapped)
                _skipRequested = true;
        }

        // ── public API ───────────────────────────────────────

        public void Open(Skill[] winners, int[] winnerLevels, Sprite[] candidatePool, Action onComplete)
        {
            _onComplete   = onComplete;
            _winners      = winners;
            _winnerLevels = winnerLevels;
            gameObject.SetActive(true);

            if (centerLotteryResult != null) centerLotteryResult.gameObject.SetActive(false);
            if (go_centerResult     != null) go_centerResult.SetActive(false);
            if (multiLotteryResults != null)
                foreach (var lr in multiLotteryResults)
                    if (lr != null) lr.gameObject.SetActive(false);
            if (go_multiResultPanel != null) go_multiResultPanel.SetActive(false);

            int skillCount = winners.Length;

            if (skillCount >= 5)
            {
                // primary slot + 4 consecutive behind (lower index, wrapping 0 → 15)
                int primary  = UnityEngine.Random.Range(0, SlotCount);
                _winnerSlots = new int[5];
                for (int i = 0; i < 5; i++)
                    _winnerSlots[i] = (primary - i + SlotCount) % SlotCount;
            }
            else
            {
                int[] shuffled = Enumerable.Range(0, SlotCount).ToArray();
                for (int i = SlotCount - 1; i > 0; i--)
                {
                    int j = UnityEngine.Random.Range(0, i + 1);
                    (shuffled[i], shuffled[j]) = (shuffled[j], shuffled[i]);
                }
                _winnerSlots = new int[skillCount];
                for (int i = 0; i < skillCount; i++)
                    _winnerSlots[i] = shuffled[i];
            }

            Sprite[] winnerSprites = new Sprite[skillCount];
            for (int i = 0; i < skillCount; i++)
            {
                var sprites = winners[i].SkillInfo.Sprite;
                bool isMax  = winnerLevels[i] >= winners[i].SkillInfo.MaxLevel;
                winnerSprites[i] = (isMax && sprites.Length > 1) ? sprites[1] : sprites[0];
            }

            AssignSlotIcons(winnerSprites, candidatePool, _winnerSlots);

            foreach (var slot in slots)
                slot.ResetGlow();

            StartCoroutine(PlaySequence(skillCount));
        }

        public void Close()
        {
            StopAllCoroutines();
            btn_continue?.onClick.RemoveAllListeners();
            _waitingForSkip = false;
            if (go_multiResultPanel != null) go_multiResultPanel.SetActive(false);
            gameObject.SetActive(false);
        }

        // ── main sequence ────────────────────────────────────

        IEnumerator PlaySequence(int skillCount)
        {
            _continuePressed = false;
            _waitingForSkip  = false;
            _skipRequested   = false;

            cg_dimPanel.alpha = 0f;
            cg_dimPanel.gameObject.SetActive(true);
            yield return cg_dimPanel.DOFade(0.7f, 0.25f).SetUpdate(true).WaitForCompletion();

            cg_popup.alpha = 0f;
            rt_popup.localScale = Vector3.one * 0.7f;
            cg_popup.gameObject.SetActive(true);
            yield return DOTween.Sequence().SetUpdate(true)
                .Join(cg_popup.DOFade(1f, AppearDuration))
                .Join(rt_popup.DOScale(Vector3.one, AppearDuration).SetEase(Ease.OutBack))
                .WaitForCompletion();

            cg_continue.alpha = 0f;
            cg_continue.blocksRaycasts = true;
            rt_continue.localScale = Vector3.one * 0.7f;
            yield return new WaitForSecondsRealtime(0.1f);
            yield return DOTween.Sequence().SetUpdate(true)
                .Join(cg_continue.DOFade(1f, AppearDuration))
                .Join(rt_continue.DOScale(Vector3.one, AppearDuration).SetEase(Ease.OutBack))
                .WaitForCompletion();

            btn_continue.onClick.AddListener(OnContinuePressed);
            yield return new WaitUntil(() => _continuePressed);
            btn_continue.onClick.RemoveAllListeners();

            // Phase-A: 5-skill has its own intro animation, so skip here
            if (skillCount == 1)
                yield return StartCoroutine(RunPhaseA());
            else if (skillCount == 3)
                yield return StartCoroutine(RunPhaseAThree());

            cg_continue.blocksRaycasts = false;
            DOTween.Sequence().SetUpdate(true)
                .Join(cg_continue.DOFade(0f, 0.2f))
                .Join(rt_continue.DOScale(Vector3.zero, 0.2f));
            go_skipHint.SetActive(true);

            if (skillCount == 1)
                yield return StartCoroutine(PlayOneSkillFlow());
            else if (skillCount == 3)
                yield return StartCoroutine(PlayThreeSkillFlow());
            else
                yield return StartCoroutine(PlayFiveSkillFlow());
        }

        // ── 1-skill flow ────────────────────────────────────

        IEnumerator PlayOneSkillFlow()
        {
            int primaryTarget = _winnerSlots[0];

            _waitingForSkip = true;
            yield return StartCoroutine(RunPhaseB());

            if (!_skipRequested)
                yield return StartCoroutine(RunPhaseC(primaryTarget));

            _waitingForSkip = false;

            if (_skipRequested)
                SnapToWinner(primaryTarget);

            for (int i = 0; i < _winners.Length; i++)
            {
                bool isMaxLevel = _winnerLevels[i] >= _winners[i].SkillInfo.MaxLevel;
                slots[_winnerSlots[i]].SetHighlight(_winners[i] is PassiveSkill, isMaxLevel);
            }

            go_skipHint.SetActive(false);

            slots[_winnerSlots[0]].ActivateWinner(Color.yellow);
            yield return new WaitForSecondsRealtime(0.8f);

            go_centerResult.SetActive(true);
            centerLotteryResult.gameObject.SetActive(true);
            centerLotteryResult.SetData(_winners[0], _winnerLevels[0]);
            cg_centerResult.alpha = 0f;
            rt_centerResult.localScale = Vector3.one * 0.7f;
            yield return DOTween.Sequence().SetUpdate(true)
                .Join(cg_centerResult.DOFade(1f, AppearDuration))
                .Join(rt_centerResult.DOScale(Vector3.one, AppearDuration).SetEase(Ease.OutBack))
                .WaitForCompletion();

            yield return StartCoroutine(ShowContinueAndWait());

            yield return DOTween.Sequence().SetUpdate(true)
                .Join(cg_dimPanel.DOFade(0f, 0.3f))
                .Join(cg_popup.DOFade(0f, 0.3f))
                .Join(cg_centerResult.DOFade(0f, 0.3f))
                .Join(cg_continue.DOFade(0f, 0.3f))
                .WaitForCompletion();

            gameObject.SetActive(false);
            _onComplete?.Invoke();
        }

        // ── 3-skill flow ─────────────────────────────────────

        IEnumerator PlayThreeSkillFlow()
        {
            // Phase-A에서 켜진 보조 빛 2개 소등 (0번, 5번, 10번 간격이므로 +5, +10)
            slots[(_currentHead +  5) % SlotCount].Deactivate();
            slots[(_currentHead + 10) % SlotCount].Deactivate();

            _waitingForSkip = true;
            int laps = UnityEngine.Random.Range(3, 5);
            int prev = _currentHead;

            for (int s = 0; s < laps * SlotCount; s++)
            {
                if (_skipRequested) break;

                int head = (_currentHead + 1) % SlotCount;
                slots[prev].Deactivate();
                slots[head].Activate();
                prev = head;
                _currentHead = head;
                yield return new WaitForSecondsRealtime(minDelay);
            }

            go_skipHint.SetActive(false);

            // RevealWinners_Three도 스킵 가능 — 내부에서 _skipRequested 처리
            yield return StartCoroutine(RevealWinners_Three());

            _waitingForSkip = false;
            yield return StartCoroutine(ShowThreeResults());
            yield return StartCoroutine(ShowContinueAndWait());

            Sequence closeSeq = DOTween.Sequence().SetUpdate(true)
                .Join(cg_dimPanel.DOFade(0f, 0.3f))
                .Join(cg_popup.DOFade(0f, 0.3f))
                .Join(cg_continue.DOFade(0f, 0.3f));

            if (cg_multiResults != null)
                foreach (var cg in cg_multiResults)
                    closeSeq.Join(cg.DOFade(0f, 0.3f));

            yield return closeSeq.WaitForCompletion();

            if (go_multiResultPanel != null) go_multiResultPanel.SetActive(false);
            gameObject.SetActive(false);
            _onComplete?.Invoke();
        }

        IEnumerator RevealWinners_Three()
        {
            for (int w = 0; w < _winners.Length; w++)
            {
                yield return StartCoroutine(RunToTarget(_winnerSlots[w]));

                // RunToTarget이 중간에 중단된 경우 현재 위치 소등
                if (_currentHead != _winnerSlots[w])
                    slots[_currentHead].Deactivate();

                bool isMax = _winnerLevels[w] >= _winners[w].SkillInfo.MaxLevel;
                slots[_winnerSlots[w]].SetHighlight(_winners[w] is PassiveSkill, isMax);
                slots[_winnerSlots[w]].ActivateWinner(Color.yellow);

                // 스킵 중이면 나머지 당첨 슬롯도 즉시 하이라이트 후 종료
                if (_skipRequested) continue;

                yield return new WaitForSecondsRealtime(0.8f);

                if (w < _winners.Length - 1 && !_skipRequested)
                    yield return StartCoroutine(RunLaps(3));
            }
        }

        IEnumerator ShowThreeResults()
        {
            if (go_multiResultPanel == null || multiLotteryResults == null) yield break;

            go_multiResultPanel.SetActive(true);

            const int StartIndex = 1;
            for (int i = 0; i < _winners.Length; i++)
            {
                int slot = StartIndex + i;
                multiLotteryResults[slot].gameObject.SetActive(true);
                multiLotteryResults[slot].SetData(_winners[i], _winnerLevels[i]);
                cg_multiResults[slot].alpha = 0f;
                rt_multiResults[slot].localScale = Vector3.one * 0.7f;
            }

            Sequence seq = DOTween.Sequence().SetUpdate(true);
            for (int i = 0; i < _winners.Length; i++)
            {
                int slot = StartIndex + i;
                float offset = i * 0.12f;
                seq.Insert(offset, cg_multiResults[slot].DOFade(1f, AppearDuration));
                seq.Insert(offset, rt_multiResults[slot].DOScale(1f, AppearDuration).SetEase(Ease.OutBack));
            }
            yield return seq.WaitForCompletion();
        }

        // ── 5-skill flow ─────────────────────────────────────

        IEnumerator PlayFiveSkillFlow()
        {
            // Steps 1-3: fill+drain from slot 10 outward to slot 2, twice
            for (int repeat = 0; repeat < 2; repeat++)
            {
                yield return StartCoroutine(FillFromSlot10());
                yield return StartCoroutine(DrainFromSlot10());
            }

            // Step 4: all slots flash 3 times
            yield return StartCoroutine(FlashAllSlots(3));

            // Step 5: build-up of trailing winner slots (winner[1] → winner[4])
            sfxSource.PlayOneShot(trainClip);
            StartCoroutine(PlayDelayedHorn(trainClip.length));
            yield return StartCoroutine(BuildUpWinnerTrail());

            // Step 6: train of 5 lights does 3-4 full laps (skip enabled)
            _waitingForSkip = true;
            yield return StartCoroutine(RunPhaseBTrain());

            // Step 7: decelerate to winner[0] — 감속 중에도 스킵 가능
            if (!_skipRequested)
                yield return StartCoroutine(RunPhaseCTrain());

            _waitingForSkip = false;

            if (_skipRequested)
                SnapToWinnerTrain();

            go_skipHint.SetActive(false);

            // Step 8: sequential winner-slot reveal (head → tail)
            yield return StartCoroutine(RevealFiveWinnersSequential());

            // Step 9: 5 result popups appear top to bottom
            yield return StartCoroutine(ShowFiveResults());

            // Step 10: continue button + close
            yield return StartCoroutine(ShowContinueAndWait());

            Sequence closeSeq = DOTween.Sequence().SetUpdate(true)
                .Join(cg_dimPanel.DOFade(0f, 0.3f))
                .Join(cg_popup.DOFade(0f, 0.3f))
                .Join(cg_continue.DOFade(0f, 0.3f));

            if (cg_multiResults != null)
                foreach (var cg in cg_multiResults)
                    closeSeq.Join(cg.DOFade(0f, 0.3f));

            yield return closeSeq.WaitForCompletion();

            if (go_multiResultPanel != null) go_multiResultPanel.SetActive(false);
            gameObject.SetActive(false);
            _onComplete?.Invoke();
        }

        IEnumerator PlayDelayedHorn(float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            sfxSource.PlayOneShot(trainClip);
        }

        // slot 10을 중심으로 양방향 확장하며 slot 2까지 켜기
        IEnumerator FillFromSlot10()
        {
            slots[10].Activate();
            yield return new WaitForSecondsRealtime(FillDrainDelay);

            for (int wave = 1; wave <= 8; wave++)
            {
                int cw  = (10 + wave) % SlotCount;
                int ccw = (10 - wave + SlotCount) % SlotCount;
                slots[cw].Activate();
                if (cw != ccw) slots[ccw].Activate();
                yield return new WaitForSecondsRealtime(FillDrainDelay);
            }
        }

        // slot 10을 중심으로 양방향 확장하며 slot 2까지 끄기
        IEnumerator DrainFromSlot10()
        {
            slots[10].Deactivate();
            yield return new WaitForSecondsRealtime(FillDrainDelay);

            for (int wave = 1; wave <= 8; wave++)
            {
                int cw  = (10 + wave) % SlotCount;
                int ccw = (10 - wave + SlotCount) % SlotCount;
                slots[cw].Deactivate();
                if (cw != ccw) slots[ccw].Deactivate();
                yield return new WaitForSecondsRealtime(FillDrainDelay);
            }
        }

        IEnumerator FlashAllSlots(int times)
        {
            for (int t = 0; t < times; t++)
            {
                foreach (var slot in slots) slot.Activate();
                yield return new WaitForSecondsRealtime(0.18f);
                foreach (var slot in slots) slot.Deactivate();
                yield return new WaitForSecondsRealtime(0.22f);
            }
        }

        // 슬롯 2→6까지 한 칸씩 켜지는 범위를 늘리다가 마지막에 5개 모두 점등된 채 종료
        IEnumerator BuildUpWinnerTrail()
        {
            const int TrainStart = 2;
            const int TrainLen   = 5;

            for (int count = 1; count <= TrainLen; count++)
            {
                for (int i = 0; i < count; i++)
                    slots[TrainStart + i].Activate();

                yield return new WaitForSecondsRealtime(BuildUpOnTime);

                if (count < TrainLen)
                {
                    for (int i = 0; i < count; i++)
                        slots[TrainStart + i].Deactivate();
                    yield return new WaitForSecondsRealtime(BuildUpGapTime);
                }
                // 마지막 라운드(5개)는 끄지 않고 그대로 유지해 돌기 시작
            }
        }

        // 빌드업이 끝난 슬롯 2~6(head=6) 상태에서 바로 3~4바퀴 회전
        IEnumerator RunPhaseBTrain()
        {
            // BuildUpWinnerTrail이 슬롯 2~6을 점등한 채 종료하므로 head는 6
            _currentHead = 6;

            int laps = UnityEngine.Random.Range(3, 5);
            for (int step = 0; step < laps * SlotCount; step++)
            {
                if (_skipRequested) break;

                int newHead = (_currentHead + 1) % SlotCount;
                int oldTail = (newHead - 5 + SlotCount) % SlotCount;
                slots[oldTail].Deactivate();
                slots[newHead].Activate();
                _currentHead = newHead;
                yield return new WaitForSecondsRealtime(minDelay);
            }
        }

        // 5개 기차가 감속하며 winner[0]에 정지
        IEnumerator RunPhaseCTrain()
        {
            int primaryTarget  = _winnerSlots[0];
            int stepsToTarget  = (primaryTarget - _currentHead + SlotCount) % SlotCount;
            if (stepsToTarget == 0) stepsToTarget = SlotCount;
            int totalSteps = SlotCount + stepsToTarget;

            for (int step = 0; step < totalSteps; step++)
            {
                if (_skipRequested) yield break;
                int newHead = (_currentHead + 1) % SlotCount;
                int oldTail = (newHead - 5 + SlotCount) % SlotCount;
                slots[oldTail].Deactivate();
                slots[newHead].Activate();
                _currentHead = newHead;

                float t     = totalSteps > 1 ? (float)step / (totalSteps - 1) : 1f;
                float delay = Mathf.Lerp(minDelay, maxDelay, decelerationCurve.Evaluate(t));
                yield return new WaitForSecondsRealtime(delay);
            }
        }

        void SnapToWinnerTrain()
        {
            for (int i = 0; i < 5; i++)
                slots[(_currentHead - i + SlotCount) % SlotCount].Deactivate();
            _currentHead = _winnerSlots[0];
            for (int i = 0; i < 5; i++)
                slots[_winnerSlots[i]].Activate();
        }

        // winner[0]부터 순서대로 당첨 이펙트
        IEnumerator RevealFiveWinnersSequential()
        {
            for (int i = 0; i < 5; i++)
            {
                bool isMax = _winnerLevels[i] >= _winners[i].SkillInfo.MaxLevel;
                slots[_winnerSlots[i]].SetHighlight(_winners[i] is PassiveSkill, isMax);
                slots[_winnerSlots[i]].ActivateWinner(Color.yellow);
                yield return new WaitForSecondsRealtime(0.35f);
            }
        }

        // 5개 결과 팝업을 위에서 아래로 순차 등장
        IEnumerator ShowFiveResults()
        {
            if (go_multiResultPanel == null || multiLotteryResults == null) yield break;

            go_multiResultPanel.SetActive(true);

            int count = Mathf.Min(5, Mathf.Min(_winners.Length, multiLotteryResults.Length));
            for (int i = 0; i < count; i++)
            {
                multiLotteryResults[i].gameObject.SetActive(true);
                multiLotteryResults[i].SetData(_winners[i], _winnerLevels[i]);
                if (i < cg_multiResults.Length) cg_multiResults[i].alpha = 0f;
                if (i < rt_multiResults.Length) rt_multiResults[i].localScale = Vector3.one * 0.7f;
            }

            Sequence seq = DOTween.Sequence().SetUpdate(true);
            for (int i = 0; i < count && i < cg_multiResults.Length; i++)
            {
                float offset = i * 0.15f;
                seq.Insert(offset, cg_multiResults[i].DOFade(1f, AppearDuration));
                seq.Insert(offset, rt_multiResults[i].DOScale(1f, AppearDuration).SetEase(Ease.OutBack));
            }
            yield return seq.WaitForCompletion();
        }

        // ── shared helpers ───────────────────────────────────

        IEnumerator ShowContinueAndWait()
        {
            _continuePressed = false;
            cg_continue.alpha = 0f;
            cg_continue.blocksRaycasts = true;
            rt_continue.localScale = Vector3.one * 0.7f;
            yield return new WaitForSecondsRealtime(0.1f);
            yield return DOTween.Sequence().SetUpdate(true)
                .Join(cg_continue.DOFade(1f, AppearDuration))
                .Join(rt_continue.DOScale(Vector3.one, AppearDuration).SetEase(Ease.OutBack))
                .WaitForCompletion();

            btn_continue.onClick.AddListener(OnContinuePressed);
            yield return new WaitUntil(() => _continuePressed);
            btn_continue.onClick.RemoveAllListeners();
        }

        void OnContinuePressed() => _continuePressed = true;

        // ── 3-skill helpers ──────────────────────────────────

        IEnumerator RunToTarget(int targetSlot)
        {
            int prev = _currentHead;
            while (_currentHead != targetSlot)
            {
                if (_skipRequested) yield break;
                int head = (_currentHead + 1) % SlotCount;
                slots[prev].Deactivate();
                slots[head].Activate();
                prev = head;
                _currentHead = head;
                yield return new WaitForSecondsRealtime(minDelay);
            }
        }

        IEnumerator RunLaps(int count)
        {
            int prev = _currentHead;
            for (int s = 0; s < count * SlotCount; s++)
            {
                if (_skipRequested) yield break;
                int head = (_currentHead + 1) % SlotCount;
                slots[prev].Deactivate();
                slots[head].Activate();
                prev = head;
                _currentHead = head;
                yield return new WaitForSecondsRealtime(minDelay);
            }
        }

        // ── 1-skill / 3-skill phase coroutines ───────────────

        // 1-skill: 단일 빛 2바퀴
        IEnumerator RunPhaseA()
        {
            _currentHead = 0;
            int prevHead = -1;

            for (int step = 0; step < 2 * SlotCount; step++)
            {
                int head = step % SlotCount;

                if (prevHead >= 0)
                    slots[prevHead].Deactivate();

                slots[head].Activate();

                prevHead = head;
                _currentHead = head;
                yield return new WaitForSecondsRealtime(minDelay);
            }
        }

        // 3-skill: 0·5·10번 슬롯에서 시작해 3개 빛이 동시에 시계방향 2바퀴
        IEnumerator RunPhaseAThree()
        {
            const int Gap3 = 5;
            _currentHead = 0;
            int prevHead = -1;

            int totalSteps = 2 * SlotCount;
            for (int step = 0; step < totalSteps; step++)
            {
                int head = step % SlotCount;

                if (prevHead >= 0)
                    for (int i = 0; i < 3; i++)
                        slots[(prevHead + i * Gap3) % SlotCount].Deactivate();

                for (int i = 0; i < 3; i++)
                    slots[(head + i * Gap3) % SlotCount].Activate();

                prevHead = head;
                _currentHead = head;

                float accelT = Mathf.Clamp01((float)step / (totalSteps - 1));
                float delay  = Mathf.Lerp(0.08f, minDelay, accelT);
                yield return new WaitForSecondsRealtime(delay);
            }
        }

        IEnumerator RunPhaseB()
        {
            int laps     = UnityEngine.Random.Range(3, 5);
            int prevHead = _currentHead;

            for (int step = 0; step < laps * SlotCount; step++)
            {
                if (_skipRequested) break;

                int head = (_currentHead + 1) % SlotCount;
                slots[prevHead].Deactivate();
                slots[head].Activate();
                prevHead = head;
                _currentHead = head;
                yield return new WaitForSecondsRealtime(minDelay);
            }
        }

        void SnapToWinner(int primaryTarget)
        {
            slots[_currentHead].Deactivate();
            _currentHead = primaryTarget;
            slots[_currentHead].Activate();
        }

        IEnumerator RunPhaseC(int primaryTarget)
        {
            int stepsToTarget = (primaryTarget - _currentHead + SlotCount) % SlotCount;
            if (stepsToTarget == 0) stepsToTarget = SlotCount;
            int totalSteps = SlotCount + stepsToTarget;
            int prevHead   = _currentHead;

            for (int step = 0; step < totalSteps; step++)
            {
                if (_skipRequested) yield break;
                int head = (_currentHead + 1) % SlotCount;
                slots[prevHead].Deactivate();
                slots[head].Activate();
                prevHead = head;
                _currentHead = head;

                float t     = totalSteps > 1 ? (float)step / (totalSteps - 1) : 1f;
                float delay = Mathf.Lerp(minDelay, maxDelay, decelerationCurve.Evaluate(t));
                yield return new WaitForSecondsRealtime(delay);
            }
        }

        // ── slot icon assignment ──────────────────────────────

        void AssignSlotIcons(Sprite[] winners, Sprite[] candidates, int[] winnerSlots)
        {
            for (int i = 0; i < winnerSlots.Length; i++)
                slots[winnerSlots[i]].SetIcon(winners[i]);

            int candidateIdx = 0;
            for (int i = 0; i < SlotCount; i++)
            {
                if (IsWinnerSlot(i, winnerSlots)) continue;
                if (candidates != null && candidates.Length > 0)
                    slots[i].SetIcon(candidates[candidateIdx++ % candidates.Length]);
            }
        }

        bool IsWinnerSlot(int index, int[] winnerSlots)
        {
            for (int i = 0; i < winnerSlots.Length; i++)
                if (winnerSlots[i] == index) return true;
            return false;
        }
    }
}
