using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class EquipmentSlot : MonoBehaviour
    {
        Equipment _equipment;
        public Equipment GetEquipment => _equipment;

        [SerializeField] Image bg;
        [SerializeField] Image icon;
        [SerializeField] TextMeshProUGUI level;
        [SerializeField] Sprite[] bgs;
        [SerializeField] Button button;
        public bool isEquipped;

        EquipmentCtrl popup;

        private void Start()
        {
            button.onClick.AddListener(() =>
            {
                PopupInfo();
            });
        }

        public void SetEquipment(Equipment equipment)
        {
            _equipment = equipment;

            bg.sprite = bgs[(int)_equipment.Tier];

            if (_equipment.Tier <= EquipmentTier.Elite)
                icon.sprite = _equipment.Data.IconSprite[0];
            else
                icon.sprite = _equipment.Data.IconSprite[1];

            UpdateSlot();
        }

        public void UpdateSlot()
        {
            level.text = "Lv." + _equipment.Level.ToString();
        }

        void PopupInfo()
        {
            popup.gameObject.SetActive(true);
            popup.SetPopup(_equipment, this);
        }

        public void SetPopup(EquipmentCtrl popup)
        {
            this.popup = popup;
        }
    }

    public class EquipmentSlotComparer : IComparer<EquipmentSlot>
    {
        // +1 = 오름 / -1 = 내림
        readonly int tierSign;     
        readonly int typeSign;
        readonly int levelSign;

        public EquipmentSlotComparer(
            bool tierAscending = false, // 기본값 : Tier 내림차순
            bool typeAscending = true, // 기본값 : Type 오름차순
            bool levelAscending = false // 기본값 : Level 내림차순
            )  
        {
            tierSign = tierAscending ? +1 : -1;
            typeSign = typeAscending ? +1 : -1;
            levelSign = levelAscending ? +1 : -1;
        }

        public int Compare(EquipmentSlot x, EquipmentSlot y)
        {
            // null 처리
            if (x.GetEquipment == null) return 1;
            if (y.GetEquipment == null) return -1;

            int result;
            // Tier 정렬
            result = tierSign * x.GetEquipment.Tier.CompareTo(y.GetEquipment.Tier);
            if (result != 0) return result;
            // Type 정렬
            result = typeSign * x.GetEquipment.Data.Type.CompareTo(y.GetEquipment.Data.Type);
            if (result != 0) return result;
            // Level 정렬
            result = levelSign * x.GetEquipment.Level.CompareTo(y.GetEquipment.Level);
            if (result != 0) return result;
            // Id 내림차순 정렬
            return y.GetEquipment.Data.Id.CompareTo(x.GetEquipment.Data.Id);
        }
    }
}