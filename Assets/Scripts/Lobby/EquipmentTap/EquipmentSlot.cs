using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ZUN
{
    public class EquipmentSlot : MonoBehaviour
    {
        Equipment equipment;

        [SerializeField] EquipmentType type;
        [SerializeField] Image bg;
        [SerializeField] Image icon;
        [SerializeField] TextMeshProUGUI level;
        [SerializeField] Sprite[] bgs;
        [SerializeField] Button button;

        EquipmentCtrlPopup popup;

        private void Start()
        {
            button.onClick.AddListener(() =>
            {
                PopupInfo();
            });
        }

        public void SetItem(Equipment _item)
        {
            equipment = _item;
            type = equipment.Data.Type;

            bg.sprite = bgs[(int)equipment.Tier];

            if (equipment.Tier <= EquipmentTier.Elite)
                icon.sprite = equipment.Data.IconSprite[0];
            else
                icon.sprite = equipment.Data.IconSprite[1];

            UpdateSlot();
        }

        public void UpdateSlot()
        {
            level.text = "Lv." + equipment.Level.ToString();
        }

        void PopupInfo()
        {
            popup.gameObject.SetActive(true);
            popup.SetPopup(equipment);
        }

        public void SetPopup(EquipmentCtrlPopup popup)
        {
            this.popup = popup;
        }
    }
}