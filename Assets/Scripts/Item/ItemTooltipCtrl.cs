using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class ItemTooltipCtrl : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI itemName;
        [SerializeField] TextMeshProUGUI tooltip;

        [SerializeField] Canvas canvas;
        RectTransform canvasRect;

        private void Awake()
        {
            canvasRect = canvas.transform as RectTransform;
        }

        public void SetTooltip(Item item, RectTransform rect)
        {
            itemName.text = item.Data.ItemName;
            tooltip.text = item.Data.Description;
        }
    }
}