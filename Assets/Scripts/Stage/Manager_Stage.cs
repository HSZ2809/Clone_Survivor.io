using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ZUN
{
    public class Manager_Stage : MonoBehaviour
    {
        Character character = null;

        [Header("Menu")]
        [SerializeField] Canvas selectWindow = null;
        [SerializeField] Btn_SelectSkill[] Options = null;

        [Header("PlayTime")]
        [SerializeField] float playTime;

        [Header("Event")]
        [SerializeField] TimedEvent[] events;

        [Header("Item List")]
        [SerializeField] ActiveSkill[] actives = null;
        [SerializeField] PassiveSkill[] passives = null;

        private readonly Dictionary<string, Skill> skillDic = new();

        [System.Serializable] public class TimedEvent
        {
            public float triggerTime;
            public UnityEvent onEvent;
        }

        public float PlayTime { get { return playTime; } }

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
        }

        private void Start()
        {
            InitSkillDic(actives, skillDic);
            InitSkillDic(passives, skillDic);
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

        public void LevelUp(ref ActiveSkill[] actives, ref PassiveSkill[] passives)
        {
            Time.timeScale = 0;

            List<string> exclusion = new List<string>();
            exclusion = GetExclusion(ref actives, ref passives);

            for (int i = 0; i < Options.Length; i++)
            {
                string skill = GetRandomSkill(ref exclusion);
                Debug.Log(skill);
                exclusion.Add(skill);

                int index = -1;
                
                index = Array.FindIndex(actives, element => element != null && element.ID == skill);

                if (index != -1)
                {
                    Options[i].id.text = actives[index].ID;
                    Options[i].image.sprite = actives[index].Sprite;
                    Options[i].upgradeInfo.text = actives[index].UpgradeInfos[actives[index].Level];
                    continue;
                }

                index = Array.FindIndex(passives, element => element != null && element.ID == skill);

                if (index != -1)
                {
                    Options[i].id.text = passives[index].ID;
                    Options[i].image.sprite = passives[index].Sprite;
                    Options[i].upgradeInfo.text = passives[index].UpgradeInfos[passives[index].Level];
                    continue;
                }

                Options[i].id.text = skillDic[skill].ID;
                Options[i].image.sprite = skillDic[skill].Sprite;
                Options[i].upgradeInfo.text = skillDic[skill].UpgradeInfos[0];

                selectWindow.gameObject.SetActive(true);
            }
        }

        private List<string> GetExclusion(ref ActiveSkill[] actives, ref PassiveSkill[] passives)
        {
            List<string> exclusion = new List<string>();
            int index = -1;

            foreach (var skill in skillDic)
            {
                index = Array.FindIndex(actives, element => element != null && element.ID == skill.Key);

                if (index > -1)
                {
                    if (actives[index].Level > 6)
                        exclusion.Add(skill.Key);
                    else if (actives[index].Level == 5)
                    {
                        string synergyID = actives[index].SynergyIN;

                        if (Array.FindIndex(passives, element => element != null && element.ID == synergyID) == -1)
                            exclusion.Add(skill.Key);
                    }
                }
                
                index = Array.FindIndex(passives, element => element != null && element.ID == skill.Key);

                if (index > -1)
                {
                    if (passives[index].Level > 5)
                        exclusion.Add(skill.Key);
                }
            }

            return exclusion;
        }

        private string GetRandomSkill(ref List<string> exclusion)
        {
            List<string> shuffle = new List<string>();

            foreach (var skill in skillDic)
            {
                if (exclusion.Contains(skill.Key))
                    continue;

                shuffle.Add(skill.Key);
            }

            int randomIndex = UnityEngine.Random.Range(0, shuffle.Count);
            string pichedID = shuffle[randomIndex];

            return pichedID;
        }

        public void SelectSkill(string skillID)
        {
            character.SetSkill(skillID);

            selectWindow.gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        public void InitSkill(string skillName)
        {
            if (skillDic.TryGetValue(skillName, out Skill skill))
            {
                Instantiate(skill, character.gameObject.transform);
            }
            else
            {
                Debug.LogWarning("Skill Not Found");
            }
        }

        public void AddSkillDic(ActiveSkill skill)
        {
            skillDic.Add(skill.SkillName, skill);
        }

        private void InitSkillDic<T>(T[] items, Dictionary<string, T> itemDictionary) where T : Skill
        {
            foreach (var item in items)
            {
                if (itemDictionary.ContainsKey(item.ID))
                {
                    Debug.LogWarning($"Duplicate ID found for {typeof(T).Name} with ID {item.ID}");
                }
                else
                {
                    itemDictionary[item.ID] = item;
                }
            }
        }
    }
}