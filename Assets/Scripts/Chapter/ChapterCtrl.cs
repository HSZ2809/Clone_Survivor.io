using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ZUN
{
    public class ChapterCtrl : MonoBehaviour
    {
        Manager_Inventory inventory;
        Character character;

        [Header("UI")]
        [SerializeField] Image selectWindow;
        [SerializeField] Btn_SelectSkill[] options;
        [SerializeField] Btn_GetMeat getMeat;
        [SerializeField] TextMeshProUGUI txt_goldCount;
        [SerializeField] TextMeshProUGUI txt_killCount;
        [SerializeField] Image[] img_ownedActive;
        [SerializeField] Image[] img_ownedPassive;
        [SerializeField] Image pnl_lottery;
        [SerializeField] LotteryResult[] lotteryResults;
        [SerializeField] Button closeLottery;
        [SerializeField] ShowResult showResult;
        [SerializeField] Image BossHpBar;
        [SerializeField] TextMeshProUGUI bossName;

        int gold;
        public int KillCount { get; private set; }
        public bool PauseTimer { get; set; }

        [Header("PlayTime")]
        [SerializeField] float playTime;

        [Header("Event")]
        [SerializeField] TimedEvent[] events;

        [Header("Appearance Item List")]
        [SerializeField] ActiveSkill[] actives = null;
        [SerializeField] PassiveSkill[] passives = null;

        [System.Serializable] public class TimedEvent
        {
            public float triggerTime;
            public UnityEvent onEvent;
        }

        public float PlayTime { get { return playTime; } }

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            inventory = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager_Inventory>();
        }

        private void Start()
        {
            transform.parent = character.transform;

            /* 인벤토리의 기본 아이템 적용 */
            actives[0] = inventory.Active;
            InitSkill(inventory.Active);
        }

        private void FixedUpdate()
        {
            if(!PauseTimer)
            {
                playTime += Time.deltaTime;

                foreach (var timedEvent in events)
                {
                    if (playTime >= timedEvent.triggerTime && timedEvent.onEvent != null)
                    {
                        timedEvent.onEvent.Invoke();
                        timedEvent.onEvent = null;
                    }
                }
            }

            // 디버그용 시간 가속 함수
            #if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 5;
            }
            #endif
        }

        public void LevelUp(ref ActiveSkill[] charActives, ref PassiveSkill[] charPassives)
        {
            Time.timeScale = 0;

            SetSkillDisplay();

            List<Skill> shuffleBox = new();

            /* insert ActiveSkill*/
            if(character.AmountOfActive > 5)
            {
                for(int i = 0; i < charActives.Length; i++)
                {
                    if (charActives[i].Level < 5)
                        shuffleBox.Add(charActives[i]);
                    else if(charActives[i].Level == 5)
                    {
                        string synergyID = charActives[i].SynergyID;

                        if (Array.FindIndex(charPassives, element => element != null && element.ID == synergyID) > -1)
                            shuffleBox.Add(charActives[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < charActives.Length; i++)
                {
                    if (charActives[i] == null)
                        continue;

                    if (charActives[i].Level < 5)
                        shuffleBox.Add(charActives[i]);
                    else if (charActives[i].Level == 5)
                    {
                        string synergyID = charActives[i].SynergyID;

                        if (Array.FindIndex(charPassives, element => element != null && element.ID == synergyID) > -1)
                            shuffleBox.Add(charActives[i]);
                    }
                }

                foreach (var skill in actives)
                {
                    bool check = false;

                    for(int i = 0; i < charActives.Length; i++)
                    {
                        if (charActives[i] == null)
                            break;

                        if (charActives[i].ID == skill.ID)
                        {
                            check = true;
                            break;
                        }
                    }

                    if(!check)
                        shuffleBox.Add(skill);
                }
            }

            /* insert PassiveSkill*/
            if (character.AmountOfPassive > 5)
            {
                for (int i = 0; i < charPassives.Length; i++)
                {
                    if (charPassives[i].Level < 5)
                        shuffleBox.Add(charPassives[i]);
                }
            }
            else
            {
                for (int i = 0; i < charPassives.Length; i++)
                {
                    if (charPassives[i] == null)
                        continue;

                    if (charPassives[i].Level < 5)
                        shuffleBox.Add(charPassives[i]);
                }

                foreach (var skill in passives)
                {
                    bool check = false;

                    for (int i = 0; i < charPassives.Length; i++)
                    {
                        if (charPassives[i] == null)
                            break;

                        if (charPassives[i].ID == skill.ID)
                        {
                            check = true;
                            break;
                        }
                    }

                    if (!check)
                        shuffleBox.Add(skill);
                }
            }

            /* 스킬 선정 */
            if(shuffleBox.Count < 1)
            {
                foreach (var option in options)
                    option.gameObject.SetActive(false);

                getMeat.gameObject.SetActive(true);

                selectWindow.gameObject.SetActive(true);
                return;
            }

            for (int i = 0; i < options.Length; i++)
            {
                if (shuffleBox.Count < 1)
                {
                    for ( ; i < options.Length; i++)
                        options[i].gameObject.SetActive(false);

                    selectWindow.gameObject.SetActive(true);

                    return;
                }

                Skill skill = GetRandomSkill(ref shuffleBox);
                shuffleBox.Remove(skill);

                options[i].skill = skill;
                options[i].gameObject.SetActive(true);
            }

            selectWindow.gameObject.SetActive(true);
        }

        public void StartLottery(ref ActiveSkill[] charActives, ref PassiveSkill[] charPassives)
        {
            Time.timeScale = 0;

            List<Skill> shuffleBox = new();

            for (int i = 0; i < charActives.Length; i++)
            {
                if (charActives[i] == null)
                    break;

                if (charActives[i].Level < 5)
                    shuffleBox.Add(charActives[i]);
                else if (charActives[i].Level == 5)
                {
                    string synergyID = charActives[i].SynergyID;

                    if (Array.FindIndex(charPassives, element => element != null && element.ID == synergyID) > -1)
                        shuffleBox.Add(charActives[i]);
                }
            }

            for (int i = 0; i < charPassives.Length; i++)
            {
                if (charPassives[i] == null)
                    break;

                if (charPassives[i].Level < 5)
                    shuffleBox.Add(charPassives[i]);
            }

            int randomInt = UnityEngine.Random.Range(0, 100);
            int amount;

            if (randomInt <= 2)
                amount = 5;
            else if (randomInt <= 20)
                amount = 3;
            else
                amount = 1;

            for (int i = 0; i < amount; i++)
            {
                if (shuffleBox.Count < 1)
                {
                    // 햄 패널 생성, 햄 제공 로직
                    // 패널 위치로 이동
                    // lotteryResults[i].gameObject.SetActive(true);
                    continue;
                }

                Skill skill = GetRandomSkill(ref shuffleBox);

                skill.Upgrade();
                lotteryResults[i].SetData(skill);
                if (skill.Level == 6)
                    shuffleBox.Remove(skill);
                // 패널 위치로 이동
                lotteryResults[i].gameObject.SetActive(true);
            }

            closeLottery.gameObject.SetActive(true);

            pnl_lottery.gameObject.SetActive(true);
        }

        private Skill GetRandomSkill(ref List<Skill> shuffleBox)
        {
            int randomIndex = UnityEngine.Random.Range(0, shuffleBox.Count);
            Skill pichedSkill = shuffleBox[randomIndex];

            return pichedSkill;
        }

        private void SetSkillDisplay()
        {
            for (int i = 0; i < character.Actives.Length; i++)
            {
                if (character.Actives[i] == null)
                    break;

                img_ownedActive[i].gameObject.SetActive(true);
                img_ownedActive[i].sprite = character.Actives[i].Sprite;
            }

            for (int i = 0; i < character.Passives.Length; i++)
            {
                if (character.Passives[i] == null)
                    break;

                img_ownedPassive[i].gameObject.SetActive(true);
                img_ownedPassive[i].sprite = character.Passives[i].Sprite;
            }
        }

        public void ShowResult()
        {
            showResult.gameObject.SetActive(true);
        }

        public void SelectSkill(Skill skill)
        {
            if (skill.Level > 0)
                skill.Upgrade();
            else
                InitSkill(skill);

            foreach (Btn_SelectSkill option in options)
                option.gameObject.SetActive(false);

            selectWindow.gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        public void CloseLottery()
        {
            foreach (LotteryResult lr in lotteryResults)
                lr.gameObject.SetActive(false);

            closeLottery.gameObject.SetActive(false);
            pnl_lottery.gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        public void InitSkill(Skill skill)
        {
            Instantiate(skill, character.gameObject.transform);
        }

        public void AddGoldCount(int gold)
        {
            this.gold += gold;
        }

        public void AddKillCount()
        {
            KillCount += 1;
            txt_killCount.text = KillCount.ToString();
        }

        /* 임시 메소드. 수정이 필요합니다. */
        public void GetMeat()
        {
            character.Hp += character.MaxHp * 0.3f;
        }
    }
}