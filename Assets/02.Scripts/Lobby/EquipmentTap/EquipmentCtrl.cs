using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class EquipmentCtrl : MonoBehaviour
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
        [SerializeField] CharacterDisplay characterDisplay;

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

        //public void Levelup()
        //{
        //    if (_equipment.Level == _equipment.Data.MaxLevel[(int)_equipment.Tier])
        //    {
        //        Debug.Log("equipment MaxLevel");
        //        return;
        //    }

        //    string scrollType = "Scroll_" + _equipment.Data.Type.ToString();
        //    int scrollAmount;
        //    if (manager_Storage.items.ContainsKey(scrollType))
        //        scrollAmount = manager_Storage.items[scrollType].Amount;
        //    else
        //        scrollAmount = 0;

        //    if (_equipment.LevelupScrollCost > scrollAmount || _equipment.LevelupGoldCost > manager_Storage.Gold)
        //    {
        //        manager_Alert.ShowPopup("Not Enough Materials");
        //        return;
        //    }

        //    manager_Storage.Gold -= _equipment.LevelupGoldCost;
        //    manager_Storage.items[scrollType].Amount -= _equipment.LevelupScrollCost;
        //    _equipment.Level += 1;

        //    if (_slot.isEquipped)
        //    {
        //        UpdateStat();
        //    }

        //    equipmentTapCtrl.UpdateItemSlots();
        //    equipmentTapCtrl.UpdateEquipSlots();
        //    equipmentTapCtrl.ResetScrollView();
        //    UpdatePopup();
        //}

        public async void LevelupAsync()
        {
            var auth = Manager_FirebaseAuth.instance;
            var userId = auth.Auth.CurrentUser.UserId;
            var storage = manager_Storage;
            var alert = manager_Alert;
            var equipCtrl = equipmentTapCtrl;
            var slot = _slot;
            var equip = _equipment;

            // 1. 최대 레벨 체크
            if (equip.Level >= equip.Data.MaxLevel[(int)equip.Tier])
            {
                Debug.Log("equipment is at max level");
                return;
            }

            // 2. 레벨업 재료 확인
            string scrollType = $"Scroll_{equip.Data.Type}";
            if (!storage.items.TryGetValue(scrollType, out var scrollItem))
            {
                alert.ShowPopup("Not Enough Materials");
                return;
            }
            int scrollAmount = scrollItem.Amount;
            if (equip.LevelupScrollCost > scrollAmount ||
                equip.LevelupGoldCost > storage.Gold)
            {
                alert.ShowPopup("Not Enough Materials");
                return;
            }

            // 3. 로컬 상태 변경 (임시)
            storage.Gold -= equip.LevelupGoldCost;
            scrollItem.Amount -= equip.LevelupScrollCost;
            equip.Level++;

            // 4-1. 메인 문서 동시 업데이트 데이터 준비
            var coreUpdates = new Dictionary<string, object>
    {
        { "Gold", storage.Gold }
    };

            // 4-2. 서브콜렉션 업데이트
            // - 장비 레벨업
            bool dbEquipOK = await UserDataManager.instance
                .UpdateEquipmentAsync(userId, equip);

            // - 메인 문서 골드 업데이트
            bool dbCoreOK = await UserDataManager.instance
                .UpdateUserCoreData(userId, coreUpdates);

            // - 아이템(스크롤) 수량 업데이트
            bool dbItemOK = await UserDataManager.instance
                .UpdateItemAsync(userId, scrollItem.Data.Id, scrollItem.Amount);

            // 5. 모두 성공했을 때만 UI/캐시 확정
            if (dbEquipOK && dbCoreOK && dbItemOK)
            {
                if (slot.isEquipped)
                    UpdateStat();

                equipCtrl.UpdateItemSlots();
                equipCtrl.UpdateEquipSlots();
                equipCtrl.ResetScrollView();
                UpdatePopup();
            }
            else
            {
                // 6. 실패 시 로컬 롤백
                storage.Gold += equip.LevelupGoldCost;
                scrollItem.Amount += equip.LevelupScrollCost;
                equip.Level--;

                alert.ShowPopup("Level up failed. Please try again.");
            }
        }

        public async void WearEquipment()
        {
            UserDataManager dataManager = UserDataManager.instance;
            Manager_FirebaseAuth AuthManager = Manager_FirebaseAuth.instance;

            string uid = AuthManager.Auth.CurrentUser.UserId;

            if(!await dataManager.EquipItemToSlot(uid, _equipment.Data.Type, _equipment.Uuid))
            {
                Debug.Log("EquipItemToSlot ERROR");
                gameObject.SetActive(false);
                return;
            }

            int itemType = (int)_equipment.Data.Type;
            if (manager_Status.Inventory[itemType] != null)
            {
                manager_Storage.equipments.Add(manager_Status.Inventory[itemType]);
                EquipmentSlot slot = equipmentTapCtrl.EquipedItemSlots[itemType];
                slot.transform.SetParent(equipmentTapCtrl.EquipmentArea, false);
                slot.isEquipped = false;
                equipmentTapCtrl.equipmentSlots.Add(slot);
            }
            manager_Storage.equipments.Remove(_equipment);
            manager_Status.Inventory[itemType] = _equipment;
            _slot.transform.SetParent(equipmentTapCtrl.InventoryTransform[itemType]);
            _slot.transform.position = equipmentTapCtrl.InventoryTransform[itemType].position;
            equipmentTapCtrl.EquipedItemSlots[itemType] = _slot;
            _slot.isEquipped = true;
            UpdateStat();
            equipmentTapCtrl.ResetScrollView();
            if (_equipment.Data.Type == EquipmentType.Weapon)
                characterDisplay.SetWeapon(_equipment.ItemSprite);
            equipmentTapCtrl.equipmentSlots.Remove(_slot);
            equipmentTapCtrl.UpdateEquipSlots();

            gameObject.SetActive(false);
        }

        public async void TakeOffEquipment()
        {
            UserDataManager dataManager = UserDataManager.instance;
            Manager_FirebaseAuth AuthManager = Manager_FirebaseAuth.instance;

            string uid = AuthManager.Auth.CurrentUser.UserId;

            if (!await dataManager.EquipItemToSlot(uid, _equipment.Data.Type, null))
            {
                Debug.Log("EquipItemToSlot ERROR");
                gameObject.SetActive(false);
                return;
            }

            _slot.transform.SetParent(equipmentTapCtrl.EquipmentArea, false);
            int itemType = (int)_equipment.Data.Type;
            manager_Storage.equipments.Add(manager_Status.Inventory[itemType]);
            manager_Status.Inventory[itemType] = null;
            _slot.isEquipped = false;
            UpdateStat();
            equipmentTapCtrl.ResetScrollView();
            equipmentTapCtrl.equipmentSlots.Add(_slot);
            equipmentTapCtrl.UpdateEquipSlots();

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