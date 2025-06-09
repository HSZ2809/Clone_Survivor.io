using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class EquipmentCtrlPopup : MonoBehaviour
    {
        [SerializeField] Image tierTitle;
        [SerializeField] Sprite[] tierTitleSprites;
        [SerializeField] TextMeshProUGUI tier;
        [SerializeField] string[] tierStrings;
        [SerializeField] TextMeshProUGUI equipmentName;
        [SerializeField] Image equipmentBg;
        [SerializeField] Sprite[] equipmentBgSprites;
        [SerializeField] Image img_equipment;
        [SerializeField] Image equipmentTypeBg;
        [SerializeField] Sprite[] equipmentTypeBgSprites;
        [SerializeField] Image equipmentType;
        [SerializeField] Sprite[] equipmentTypeSprites;
        [SerializeField] TextMeshProUGUI tmp_level;
        [SerializeField] Image img_stat;
        [SerializeField] Sprite[] stats;
        [SerializeField] TextMeshProUGUI tmp_stat;
        [SerializeField] TextMeshProUGUI tmp_description;
        [SerializeField] SkillDescriptionCtrl[] skillDescriptionCtrls;
        [SerializeField] GameObject CurrencyTap;
        [SerializeField] TextMeshProUGUI tmp_gold;
        [SerializeField] TextMeshProUGUI tmp_scroll;
        [SerializeField] Image img_scroll;
        [SerializeField] Sprite[] scrollSprites;
        [SerializeField] TextMeshProUGUI tmp_maxLevelNotice;
        [SerializeField] Button btn_equip;
        [SerializeField] Button btn_unequip;
        [SerializeField] Button equipLevelup;
        [SerializeField] Button equipLevelToMax;

        [Space]
        [SerializeField] EquipmentTapCtrl equipmentTapCtrl;
        Equipment _equipment;
        EquipmentSlot _slot;
        Manager_Storage manager_Storage;
        Manager_Status manager_Status;
        Manager_Alert manager_Alert;

        const int ATK = 0, HP = 1;

        private void Awake()
        {
            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_Storage>(out manager_Storage))
                Debug.LogWarning("Manager_Storage not found");

            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_Status>(out manager_Status))
                Debug.LogWarning("Manager_Storage not found");

            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_Alert>(out manager_Alert))
                Debug.LogWarning("Manager_Alert not found");
        }

        public void SetPopup(Equipment equipment, EquipmentSlot slot)
        {
            _equipment = equipment;
            _slot = slot;

            tierTitle.sprite = tierTitleSprites[(int)_equipment.Tier];
            tier.text = tierStrings[(int)_equipment.Tier];
            equipmentName.text = _equipment.Data.EquipmentName;
            equipmentBg.sprite = equipmentBgSprites[(int)_equipment.Tier];
            img_equipment.sprite = (_equipment.Tier < EquipmentTier.Elite) ? _equipment.Data.IconSprite[0] : _equipment.Data.IconSprite[1];
            equipmentTypeBg.sprite = equipmentTypeBgSprites[(int)_equipment.Tier];
            equipmentType.sprite = equipmentTypeSprites[(int)_equipment.Data.Type];
            //tmp_stat.text = _equipment.Stat.ToString();
            if ((int)_equipment.Data.Type % 2 == ATK)
                img_stat.sprite = stats[ATK];
            else
                img_stat.sprite = stats[HP];
            tmp_description.text = _equipment.Data.Comment;
            string[] tierSkillDescription = _equipment.Data.GetTierSkillDescription();
            for (int i = 0; i < skillDescriptionCtrls.Length; i++)
            {
                if (tierSkillDescription[i] == "EMPTY")
                    skillDescriptionCtrls[i].gameObject.SetActive(false);
                else
                {
                    skillDescriptionCtrls[i].SetDescription(tierSkillDescription[i]);
                    skillDescriptionCtrls[i].gameObject.SetActive(true);
                }
            }
            for(int i = 0; i < (int)_equipment.Tier; i++)
            {
                skillDescriptionCtrls[i].Unlock();
            }
            img_scroll.sprite = scrollSprites[(int)_equipment.Data.Type];
            if (_slot.isEquipped)
            {
                btn_unequip.gameObject.SetActive(true);
                btn_equip.gameObject.SetActive(false);
            }
            else
            {
                btn_equip.gameObject.SetActive(true);
                btn_unequip.gameObject.SetActive(false);
            }      

                UpdatePopup();
        }

        void UpdatePopup()
        {
            tmp_stat.text = _equipment.Stat.ToString();

            string scrollType = "Scroll_" + _equipment.Data.Type.ToString();
            int scrollAmount;
            if (manager_Storage.items.ContainsKey(scrollType))
                scrollAmount = manager_Storage.items[scrollType].Amount;
            else
                scrollAmount = 0;

            tmp_scroll.text = _equipment.LevelupScrollCost.ToString() + "/" + scrollAmount;
            if (_equipment.LevelupScrollCost > scrollAmount)
                tmp_scroll.color = Color.red;
            else
                tmp_scroll.color = Color.white;

            tmp_gold.text = _equipment.LevelupGoldCost.ToString() + "/" + manager_Storage.GetGoldFormatKNotation();
            if (_equipment.LevelupGoldCost > manager_Storage.Gold)
                tmp_gold.color = Color.red;
            else
                tmp_gold.color = Color.white;

            _slot.UpdateSlot();
            tmp_level.text = "레벨: " + _equipment.Level.ToString() + "/" + _equipment.Data.MaxLevel[(int)_equipment.Tier].ToString();

            if (_equipment.Level == _equipment.Data.MaxLevel[(int)_equipment.Tier])
            {
                CurrencyTap.SetActive(false);
                equipLevelup.gameObject.SetActive(false);
                equipLevelToMax.gameObject.SetActive(false);
                tmp_maxLevelNotice.gameObject.SetActive(true);
            }
            else
            {
                CurrencyTap.SetActive(true);
                equipLevelup.gameObject.SetActive(true);
                equipLevelToMax.gameObject.SetActive(true);
                tmp_maxLevelNotice.gameObject.SetActive(false);
            }
        }

        public void Levelup()
        {
            if (_equipment.Level == _equipment.Data.MaxLevel[(int)_equipment.Tier])
            {
                Debug.Log("equipment MaxLevel");
                return;
            }

            string scrollType = "Scroll_" + _equipment.Data.Type.ToString();
            int scrollAmount;
            if (manager_Storage.items.ContainsKey(scrollType))
                scrollAmount = manager_Storage.items[scrollType].Amount;
            else
                scrollAmount = 0;

            if (_equipment.LevelupScrollCost > scrollAmount || _equipment.LevelupGoldCost > manager_Storage.Gold)
            {
                manager_Alert.ShowPopup("업그레이드 재료 부족!");
                return;
            }

            manager_Storage.Gold -= _equipment.LevelupGoldCost;
            manager_Storage.items[scrollType].Amount -= _equipment.LevelupScrollCost;
            _equipment.Level += 1;

            if (_slot.isEquipped)
            {
                UpdateStat();
            }

            UpdatePopup();
        }

        public void WearEquipment()
        {
            int itemType = (int)_equipment.Data.Type;
            if (manager_Status.Inventory[itemType] != null)
            {
                manager_Storage.equipments.Add(manager_Status.Inventory[itemType]);
                EquipmentSlot slotTrans = equipmentTapCtrl.EquipedItemSlots[itemType];
                slotTrans.transform.SetParent(equipmentTapCtrl.EquipmentArea, false);
                slotTrans.isEquipped = false;
            }
            manager_Status.Inventory[itemType] = _equipment;
            _slot.transform.SetParent(equipmentTapCtrl.InventoryTransform[itemType]);
            _slot.transform.position = equipmentTapCtrl.InventoryTransform[itemType].position;
            equipmentTapCtrl.EquipedItemSlots[itemType] = _slot;
            _slot.isEquipped = true;
            UpdateStat();

            gameObject.SetActive(false);
        }

        public  void TakeOffEquipment()
        {
            _slot.transform.SetParent(equipmentTapCtrl.EquipmentArea, false);
            int itemType = (int)_equipment.Data.Type;
            manager_Storage.equipments.Add(manager_Status.Inventory[itemType]);
            manager_Status.Inventory[itemType] = null;
            _slot.isEquipped = false;
            UpdateStat();

            gameObject.SetActive(false);
        }

        void UpdateStat()
        {
            if ((int)_equipment.Data.Type % 2 == ATK)
                equipmentTapCtrl.SetAtk();
            else
                equipmentTapCtrl.SetHp();
        }
    }
}