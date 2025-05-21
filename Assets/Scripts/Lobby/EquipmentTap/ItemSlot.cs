using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ZUN
{
    public class ItemSlot : MonoBehaviour
    {
        Equipment equipment;

        [SerializeField] Image bg;
        [SerializeField] Image icon;
        [SerializeField] TextMeshProUGUI level;

        [SerializeField] Sprite[] bgs;

        public void SetItem(Equipment _item)
        {
            equipment = _item;

            bg.sprite = bgs[(int)equipment.Tier];

            if (equipment.Tier <= EquipmentTier.Elite)
                icon.sprite = equipment.Data.IconSprite[0];
            else
                icon.sprite = equipment.Data.IconSprite[1];

            level.text = equipment.Level.ToString();
        }

        public void UpdateSlot()
        {
            level.text = equipment.Level.ToString();
        }

        public void PopupInfo()
        {
            // 아이템 설명 + 강화 창 팝업
        }
    }
}