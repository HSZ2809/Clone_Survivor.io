using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ZUN
{
    public class EquipmentSlot : MonoBehaviour
    {
        Equipment _equipment;

        [SerializeField] Image bg;
        [SerializeField] Image icon;
        [SerializeField] TextMeshProUGUI level;
        [SerializeField] Sprite[] bgs;
        [SerializeField] Button button;
        public bool isEquipped;

        EquipmentCtrlPopup popup;

        private void Start()
        {
            button.onClick.AddListener(() =>
            {
                PopupInfo();
            });
        }

        public void SetItem(Equipment equipment)
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

        public void SetPopup(EquipmentCtrlPopup popup)
        {
            this.popup = popup;
        }
    }
}