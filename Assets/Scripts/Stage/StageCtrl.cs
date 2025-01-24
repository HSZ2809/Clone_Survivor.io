using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

namespace ZUN
{
    public class StageCtrl : MonoBehaviour
    {
        Manager_Inventory inventory;
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
            inventory = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager_Inventory>();
        }

        private void Start()
        {
            actives[0] = inventory.Active;

            InitSkillDic(actives, skillDic);
            InitSkillDic(passives, skillDic);

            InitSkill(inventory.Active.ID);
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

            List<string> shuffleBox = new List<string>();

            /* insert ActiveSkill*/
            if(character.AmountOfActive > 5)
            {
                for(int i = 0; i < charActives.Length; i++)
                {
                    if (charActives[i].Level < 5)
                        shuffleBox.Add(charActives[i].ID);
                    else if(charActives[i].Level == 5)
                    {
                        string synergyID = charActives[i].SynergyID;

                        if (Array.FindIndex(charPassives, element => element != null && element.ID == synergyID) != -1)
                            shuffleBox.Add(charActives[i].ID);
                    }
                }
            }
            else
            {
                foreach (var skill in actives)
                {
                    shuffleBox.Add(skill.ID);
                }

                for (int i = 0; i < charActives.Length; i++)
                {
                    if (charActives[i] == null)
                        break;

                    if (charActives[i].Level > 5)
                        shuffleBox.Remove(charActives[i].ID);
                    else if (charActives[i].Level == 5)
                    {
                        string synergyID = charActives[i].SynergyID;

                        if (Array.FindIndex(charPassives, element => element != null && element.ID == synergyID) == -1)
                            shuffleBox.Remove(charActives[i].ID);
                    }
                }
            }
            /* insert PassiveSkill*/
            if (character.AmountOfPassive > 5)
            {
                for (int i = 0; i < charPassives.Length; i++)
                {
                    if (charPassives[i].Level < 5)
                        shuffleBox.Add(charPassives[i].ID);
                }
            }
            else
            {
                foreach (var skill in passives)
                {
                    shuffleBox.Add(skill.ID);
                }

                for (int i = 0; i < charPassives.Length; i++)
                {
                    if (charPassives[i] == null)
                        break;

                    if (charPassives[i].Level >= 5)
                        shuffleBox.Remove(charPassives[i].ID);
                }
            }

            if(shuffleBox.Count < 1)
            {
                foreach (var option in Options)
                    option.gameObject.SetActive(false);

                // 고기 제공

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

                int index = -1;
                string skillID = GetRandomSkill(ref shuffleBox);

                shuffleBox.Remove(skillID);

                index = Array.FindIndex(charActives, element => element != null && element.ID == skillID);

                if (index != -1)
                {
                    Options[i].id.text = charActives[index].ID;
                    Options[i].image.sprite = charActives[index].Sprite;
                    Options[i].upgradeInfo.text = charActives[index].UpgradeInfos[charActives[index].Level];
                    Options[i].gameObject.SetActive(true);
                    continue;
                }

                index = Array.FindIndex(charPassives, element => element != null && element.ID == skillID);

                if (index != -1)
                {
                    Options[i].id.text = charPassives[index].ID;
                    Options[i].image.sprite = charPassives[index].Sprite;
                    Options[i].upgradeInfo.text = charPassives[index].UpgradeInfos[charPassives[index].Level];
                    Options[i].gameObject.SetActive(true);
                    continue;
                }

                Options[i].id.text = skillDic[skillID].ID;
                Options[i].image.sprite = skillDic[skillID].Sprite;
                Options[i].upgradeInfo.text = skillDic[skillID].UpgradeInfos[0];

                Options[i].gameObject.SetActive(true);
            }

            selectWindow.gameObject.SetActive(true);
        }

        //private List<string> GetExclusion(ref ActiveSkill[] actives, ref PassiveSkill[] passives)
        //{
        //    List<string> exclusion = new List<string>();
        //    int index = -1;

        //    foreach (var skill in skillDic)
        //    {
        //        index = Array.FindIndex(actives, element => element != null && element.ID == skill.Key);

        //        if (index > -1)
        //        {
        //            if (actives[index].Level > 6)
        //                exclusion.Add(skill.Key);
        //            else if (actives[index].Level == 5)
        //            {
        //                string synergyID = actives[index].SynergyIN;

        //                if (Array.FindIndex(passives, element => element != null && element.ID == synergyID) == -1)
        //                    exclusion.Add(skill.Key);
        //            }
        //        }
                
        //        index = Array.FindIndex(passives, element => element != null && element.ID == skill.Key);

        //        if (index > -1)
        //        {
        //            if (passives[index].Level > 5)
        //                exclusion.Add(skill.Key);
        //        }
        //    }

        //    return exclusion;
        //}

        private string GetRandomSkill(ref List<string> shuffleBox)
        {
            int randomIndex = UnityEngine.Random.Range(0, shuffleBox.Count);
            string pichedID = shuffleBox[randomIndex];

            return pichedID;
        }

        public void SelectSkill(string skillID)
        {
            character.SetSkill(skillID);

            selectWindow.gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        public void InitSkill(string skillID)
        {
            if (skillDic.TryGetValue(skillID, out Skill skill))
            {
                Instantiate(skill, character.gameObject.transform);
            }
            else
            {
                Debug.LogWarning("Skill Not Found");
            }
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