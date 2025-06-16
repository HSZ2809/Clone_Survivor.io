using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class ItemSlot : MonoBehaviour
    {
        Item _item;

        [SerializeField] Image bg;
        [SerializeField] Image icon;
        [SerializeField] TextMeshProUGUI amount;
        [SerializeField] Button button;

        ItemTooltipCtrl _tooltip;

        private void Start()
        {
            button.onClick.AddListener(() =>
            {
                ShowTooltip();
            });
        }
        void ShowTooltip()
        {
            _tooltip.gameObject.SetActive(true);
            _tooltip.SetTooltip(_item, transform);
        }

        public void SetItem(Item item)
        {
            _item = item;

            bg.sprite = _item.Data.Background;
            icon.sprite = _item.Data.ItemSprite;
            amount.text = _item.Amount.ToString();
        }

        public void UpdateSlot()
        {
            amount.text = _item.Amount.ToString();
        }

        public void SetTooltip(ItemTooltipCtrl tooltip)
        {
            _tooltip = tooltip;
        }
    }
}