using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ZUN
{
    public class MissionCtrl : MonoBehaviour
    {
        Manager_Inventory inventory;
        Character character = null;

        [Header("UI")]
        [SerializeField] Canvas selectWindow = null;
        [SerializeField] Btn_SelectSkill[] Options = null;
        [SerializeField] TextMeshProUGUI txt_goldCount;
        [SerializeField] TextMeshProUGUI txt_killCount;
        [SerializeField] Image[] img_ownedActive;
        [SerializeField] Image[] img_ownedPassive;

        int gold;
        int kill;

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
            /* 인벤토리의 기본 아이템 적용 */
            actives[0] = inventory.Active;
            InitSkill(inventory.Active);
        }

        private void FixedUpdate()
        {
            playTime += Time.deltaTime;

            foreach(var timedEvent in events)
            {
                if(playTime >= timedEvent.triggerTime && timedEvent.onEvent != null)
                {
                    timedEvent.onEvent.Invoke();
                    timedEvent.onEvent = null;
                }
            }
        }

        public void LevelUp(ref ActiveSkill[] charActives, ref PassiveSkill[] charPassives)
        {
            Time.timeScale = 0;

            List<Skill> shuffleBox = new List<Skill>();

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

                    for(int i = 0; i < shuffleBox.Count; i++)
                    {
                        if(shuffleBox[i].ID == skill.ID)
                            check = true;
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

                    for (int i = 0; i < shuffleBox.Count; i++)
                    {
                        if (shuffleBox[i].ID == skill.ID)
                            check = true;
                    }

                    if (!check)
                        shuffleBox.Add(skill);
                }
            }

            if(shuffleBox.Count < 1)
            {
                foreach (var option in Options)
                    option.gameObject.SetActive(false);

                /* 고기 제공 */

                selectWindow.gameObject.SetActive(true);
                return;
            }

            for (int i = 0; i < Options.Length; i++)
            {
                if (shuffleBox.Count < 1)
                {
                    for ( ; i < Options.Length; i++)
                        Options[i].gameObject.SetActive(false);

                    selectWindow.gameObject.SetActive(true);

                    return;
                }

                Skill skill = GetRandomSkill(ref shuffleBox);
                shuffleBox.Remove(skill);

                Options[i].skill = skill;
                Options[i].gameObject.SetActive(true);
            }

            selectWindow.gameObject.SetActive(true);
        }

        private Skill GetRandomSkill(ref List<Skill> shuffleBox)
        {
            int randomIndex = UnityEngine.Random.Range(0, shuffleBox.Count);
            Skill pichedSkill = shuffleBox[randomIndex];

            return pichedSkill;
        }

        public void SelectSkill(Skill skill)
        {
            if (skill.Level > 0)
                skill.Upgrade();
            else
                InitSkill(skill);

            selectWindow.gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        public void InitSkill(Skill skill)
        {
            skill = Instantiate(skill, character.gameObject.transform);

            if (skill.Type == Skill.SkillType.ACTIVE)
            {
                img_ownedActive[skill.Order].sprite = skill.Sprite;
                img_ownedActive[skill.Order].gameObject.SetActive(true);
            }
            else
            {
                img_ownedPassive[skill.Order].sprite = skill.Sprite;
                img_ownedPassive[skill.Order].gameObject.SetActive(true);
            }
        }

        public void AddGoldCount(int gold)
        {
            this.gold += gold;
        }

        public void AddKillCount()
        {
            kill += 1;
            txt_killCount.text = kill.ToString();
        }

        /* 임시 메소드. 수정이 필요합니다. */
        public void GetMeat()
        {
            character.Hp += character.MaxHp * 0.3f;
        }
    }
}