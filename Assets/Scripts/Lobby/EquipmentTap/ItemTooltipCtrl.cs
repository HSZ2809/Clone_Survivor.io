//using TMPro;
//using UnityEngine;

//namespace ZUN
//{
//    public class ItemTooltipCtrl : MonoBehaviour
//    {
//        [SerializeField] TextMeshProUGUI itemName;
//        [SerializeField] TextMeshProUGUI tooltip;

//        [SerializeField] Canvas canvas;

//        public void SetTooltip(Item item, Transform slotTransform)
//        {
//            itemName.text = item.Data.ItemName;
//            tooltip.text = item.Data.Description;
//        }
//    }
//}

using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ZUN
{
    public class ItemTooltipCtrl : MonoBehaviour, IPointerDownHandler

    {
        [SerializeField] private Image tooltipBase;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI tooltip;

        [SerializeField] private Canvas canvas;
        private RectTransform canvasRect;
        private RectTransform tooltipBaseRect;

        // 수평 마진 값을 추가 (픽셀 단위)
        [SerializeField] private float horizontalMargin = 20f;

        private void Awake()
        {
            canvasRect = canvas.GetComponent<RectTransform>();
            tooltipBaseRect = tooltipBase.GetComponent<RectTransform>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            gameObject.SetActive(false);
        }

        public void SetTooltip(Item item, Transform slotTransform)
        {
            itemName.text = item.Data.ItemName;
            tooltip.text = item.Data.Description;

            RectTransform slotRect = slotTransform as RectTransform;
            if (slotRect == null)
                return;

            // 1. 슬롯의 월드 위치를 스크린 포인트로 변환
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(null, slotRect.position);

            // 2. 스크린 포인트를 캔버스의 로컬 좌표로 변환
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, canvas.worldCamera, out localPoint);

            // 3. 슬롯 위쪽에 오프셋 적용 (툴팁의 높이와 슬롯의 높이, 추가적인 여백 고려)
            float offsetY = slotRect.rect.height * 0.5f + tooltipBaseRect.rect.height * (1 - tooltipBaseRect.pivot.y) + 10f;
            Vector2 desiredPos = localPoint + new Vector2(0, offsetY);

            // 4. 캔버스 로컬 사각형 내에서 위치 Clamp (수평 마진 적용)
            Vector2 minPosition = new Vector2(
                canvasRect.rect.xMin + tooltipBaseRect.rect.width * tooltipBaseRect.pivot.x + horizontalMargin,
                canvasRect.rect.yMin + tooltipBaseRect.rect.height * tooltipBaseRect.pivot.y);
            Vector2 maxPosition = new Vector2(
                canvasRect.rect.xMax - tooltipBaseRect.rect.width * (1 - tooltipBaseRect.pivot.x) - horizontalMargin,
                canvasRect.rect.yMax - tooltipBaseRect.rect.height * (1 - tooltipBaseRect.pivot.y));

            desiredPos.x = Mathf.Clamp(desiredPos.x, minPosition.x, maxPosition.x);
            desiredPos.y = Mathf.Clamp(desiredPos.y, minPosition.y, maxPosition.y);

            // 5. 최종 위치를 anchoredPosition에 적용
            tooltipBaseRect.anchoredPosition = desiredPos;
        }
    }
}