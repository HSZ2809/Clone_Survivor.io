using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class ChapterCtrl : MonoBehaviour
    {
        Manager_Inventory inventory;
        Character character;

        [Header("UI")]
        [SerializeField] GameObject selectWindow;
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
        [SerializeField] GameObject levelUi;
        [SerializeField] Animator anim_levelUpReward;
        [SerializeField] AnimationClip closeRewardPage;

        public int gold;
        public int KillCount { get; private set; }

        [Header("Appearance Item List")]
        [SerializeField] ActiveSkill[] actives = null;
        [SerializeField] PassiveSkill[] passives = null;

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            inventory = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager_Inventory>();
        }

        private void Start()
        {
            // transform.parent = character.transform;

            /* 인벤토리의 기본 아이템 적용 */
            actives[0] = inventory.Active;
            InitSkill(inventory.Active);
        }

        private void Update()
        {
            // 디버그용 시간 가속
            #if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(Time.timeScale <= 1f)
                    Time.timeScale = 5f;
                else
                    Time.timeScale = 1f;
            }
            #endif
        }

        public void ShowLevelUpReward(ref ActiveSkill[] charActives, ref PassiveSkill[] charPassives)
        {
            Time.timeScale = 0;
            
            levelUi.transform.SetAsLastSibling();
            SetSkillDisplay();
            anim_levelUpReward.SetTrigger("OpenRewardPage");

            List<Skill> shuffleBox = new();

            /* insert ActiveSkill*/
            if(character.AmountOfActive > 5)
            {
                for(int i = 0; i < charActives.Length; i++)
                {
                    if (charActives[i].Level < charActives[i].SkillInfo.MaxLevel)
                        shuffleBox.Add(charActives[i]);
                    else if(charActives[i].Level == charActives[i].SkillInfo.MaxLevel)
                    {
                        string synergyID = charActives[i].SynergyID;

                        if (Array.FindIndex(charPassives, element => element != null && element.SkillInfo.ID == synergyID) > -1)
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

                    if (charActives[i].Level < charActives[i].SkillInfo.MaxLevel)
                        shuffleBox.Add(charActives[i]);
                    else if (charActives[i].Level == charActives[i].SkillInfo.MaxLevel)
                    {
                        string synergyID = charActives[i].SynergyID;

                        if (Array.FindIndex(charPassives, element => element != null && element.SkillInfo.ID == synergyID) > -1)
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

                        if (charActives[i].SkillInfo.ID == skill.SkillInfo.ID)
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

                    if (charPassives[i].Level < charPassives[i].SkillInfo.MaxLevel)
                        shuffleBox.Add(charPassives[i]);
                }

                foreach (var skill in passives)
                {
                    bool check = false;

                    for (int i = 0; i < charPassives.Length; i++)
                    {
                        if (charPassives[i] == null)
                            break;

                        if (charPassives[i].SkillInfo.ID == skill.SkillInfo.ID)
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

                //selectWindow.SetActive(true);
                return;
            }

            for (int i = 0; i < options.Length; i++)
            {
                if (shuffleBox.Count < 1)
                {
                    for ( ; i < options.Length; i++)
                        options[i].gameObject.SetActive(false);

                    //selectWindow.SetActive(true);

                    return;
                }

                Skill skill = GetRandomSkill(ref shuffleBox);
                shuffleBox.Remove(skill);

                options[i].SetSkill(skill);
                options[i].gameObject.SetActive(true);
            }

            //selectWindow.SetActive(true);
        }

        public void StartLottery(ref ActiveSkill[] charActives, ref PassiveSkill[] charPassives)
        {
            Time.timeScale = 0;

            List<Skill> shuffleBox = new();

            for (int i = 0; i < charActives.Length; i++)
            {
                if (charActives[i] == null)
                    break;

                if (charActives[i].Level < charActives[i].SkillInfo.MaxLevel)
                    shuffleBox.Add(charActives[i]);
                else if (charActives[i].Level == charActives[i].SkillInfo.MaxLevel)
                {
                    string synergyID = charActives[i].SynergyID;

                    if (Array.FindIndex(charPassives, element => element != null && element.SkillInfo.ID == synergyID) > -1)
                        shuffleBox.Add(charActives[i]);
                }
            }

            for (int i = 0; i < charPassives.Length; i++)
            {
                if (charPassives[i] == null)
                    break;

                if (charPassives[i].Level < charPassives[i].SkillInfo.MaxLevel)
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
                if (skill.Level == skill.SkillInfo.MaxLevel)
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
                img_ownedActive[i].sprite = character.Actives[i].SkillInfo.Sprite;
            }

            for (int i = 0; i < character.Passives.Length; i++)
            {
                if (character.Passives[i] == null)
                    break;

                img_ownedPassive[i].gameObject.SetActive(true);
                img_ownedPassive[i].sprite = character.Passives[i].SkillInfo.Sprite;
            }
        }

        public void ShowResult()
        {
            Time.timeScale = 0.0f;
            showResult.gameObject.SetActive(true);
        }

        public void SelectSkill(Skill skill)
        {
            if (skill.Level > 0)
                skill.Upgrade();
            else
                InitSkill(skill);

            StartCoroutine(CloseRewardPage());
        }

        IEnumerator CloseRewardPage()
        {
            anim_levelUpReward.SetTrigger("CloseRewardPage");
            yield return new WaitForSecondsRealtime(closeRewardPage.length);

            // selectWindow.SetActive(false);
            levelUi.transform.SetAsFirstSibling();
            character.LevelUpCheck();

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