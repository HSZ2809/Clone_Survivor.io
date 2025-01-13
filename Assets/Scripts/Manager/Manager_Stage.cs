using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class Manager_Stage : MonoBehaviour
    {
        [Header("Connected Components")]
        [SerializeField] private Canvas selectWindow = null;
        [SerializeField] private Btn_SelectItem[] Btn_Options = null;
        private Character character = null;

        [Header("Item List")]
        [SerializeField] private Weapon[] weapons = null;
        [SerializeField] private Passive[] passives = null;

        private readonly Dictionary<string, Item> itemDictionary = new();

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();

            FillItemDictionary(weapons, itemDictionary);
            FillItemDictionary(passives, itemDictionary);
        }

        private void Start()
        {
            SpawnInitialWeapon();
        }

        public void ShowOptions(int amountOfWeapon, string[] WISNs, int amountOfPassive)
        {
            Time.timeScale = 0;

            selectWindow.gameObject.SetActive(true);

            List<string> shuffle = new List<string>();

            foreach (var item in itemDictionary)
            {
                shuffle.Add(item.Key);
            }
            
            for (int i = 0; i < Btn_Options.Length; i++)
            {
                int randomIndex = Random.Range(0, shuffle.Count);
                string pichedSN = shuffle[randomIndex];

                itemDictionary.TryGetValue(pichedSN, out Item item);

                shuffle.RemoveAt(randomIndex);

                Btn_Options[i].image.sprite = item.Sprite;
                Btn_Options[i].textMeshProUGUI.text = item.UpgradeInfos[0];
                Btn_Options[i].itemSN = item.SerialNumber;
            }
        }

        private void FillItemDictionary<T>(T[] items, Dictionary<string, T> itemDictionary) where T : Item
        {
            foreach (var item in items)
            {
                if (itemDictionary.ContainsKey(item.SerialNumber))
                {
                    Debug.LogWarning($"Duplicate SerialNumber found for {typeof(T).Name} with SerialNumber {item.SerialNumber}");
                }
                else
                {
                    itemDictionary[item.SerialNumber] = item;
                }
            }
        }

        private void SpawnInitialWeapon()
        {
            if (itemDictionary.TryGetValue(character.InitialWeaponSN, out Item initialWeapon))
            {
                Instantiate(initialWeapon, character.gameObject.transform);
            }
            else
            {
                Debug.LogWarning("Initial Weapon Not Found");
            }
        }

        public void SelectItem(string _itemSN)
        {


            selectWindow.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}