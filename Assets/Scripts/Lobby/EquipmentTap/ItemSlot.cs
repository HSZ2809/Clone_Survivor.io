using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ZUN
{
    public class ItemSlot : MonoBehaviour
    {
        Item item;

        [SerializeField] Image           bg;
        [SerializeField] Image           icon;
        [SerializeField] TextMeshProUGUI amount;

        [Space]
        [SerializeField] Image           tooltipBg;
        [SerializeField] TextMeshProUGUI tooltip;

        public void SetItem(Item _item)
        {
            item = _item;
            icon.sprite = item.Data.IconSprite;
            tooltip.text = item.Data.Tooltip;
            amount.text = item.Amount.ToString();
        }

        public void UpdateSlot()
        {
            amount.text = item.Amount.ToString();
        }
    }
}
