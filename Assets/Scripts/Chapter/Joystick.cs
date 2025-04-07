using UnityEngine;
using UnityEngine.EventSystems;

namespace ZUN
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private Camera mainCam;
        [SerializeField] private RectTransform canvasRect;
        [SerializeField] private RectTransform lever;
        [SerializeField] private RectTransform stickBase;
        private Vector2 baseOriginalLocation;
        private Vector2 leverCenter;
        private float radius;
        private readonly float joystickSize = 25.0f;

        private void Awake()
        {
            mainCam = Camera.main;
        }

        private void Start()
        {
            baseOriginalLocation = stickBase.anchoredPosition;
            leverCenter = stickBase.anchoredPosition;
            radius = Screen.height / joystickSize;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect, eventData.position, mainCam, out Vector2 localPoint))
            {
                LocationSet(localPoint);
            }
            else
            {
                Debug.Log("OnPointerDown Error");
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect, eventData.position, mainCam, out Vector2 localPoint))
            {
                ProcessInput(localPoint);
            }
            else
            {
                Debug.Log("OnDrag Error");
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            lever.anchoredPosition = Vector2.zero;

            BackOriginalLocation();
            UpdatePositionValues();
        }

        private void LocationSet(Vector2 eventData)
        {
            //stickBase.transform.position = eventData;

            stickBase.anchoredPosition = eventData;
            leverCenter = eventData;
        }

        private void BackOriginalLocation()
        {
            stickBase.anchoredPosition = baseOriginalLocation;
            leverCenter = baseOriginalLocation;
        }

        private void ProcessInput(Vector2 inputPosition)
        {
            Vector2 tempVector = inputPosition - leverCenter;

            tempVector = Vector2.ClampMagnitude(tempVector, radius);

            //lever.transform.position = (Vector2)leverCenter + tempVector;
            lever.anchoredPosition = tempVector;

            UpdatePositionValues();
        }

        public float HorizontalAxis
        {
            private set;
            get;
        }

        public float VerticalAxis
        {
            private set;
            get;
        }

        private void UpdatePositionValues()
        {
            Vector2 leverPosition = (lever.anchoredPosition) / radius;

            HorizontalAxis = leverPosition.x;
            VerticalAxis = leverPosition.y;
        }

        public float GetHorizontalAxis()
        {
            return HorizontalAxis;
        }

        public float GetVerticalAxis()
        {
            return VerticalAxis;
        }
    }
}