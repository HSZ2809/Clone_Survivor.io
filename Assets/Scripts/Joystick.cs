using UnityEngine;
using UnityEngine.EventSystems;

namespace ZUN
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform lever = new RectTransform();
        [SerializeField] private RectTransform stickBase = new RectTransform();
        private Vector3 leverCenter = Vector3.zero;
        private float radius = 1.0f;
        private float deadZone = 0.0f;
        private float joystickSize = 20.0f;

        private void Start()
        {
            leverCenter = stickBase.position;
            radius = Screen.height / joystickSize;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ProcessInput(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            ProcessInput(eventData.position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            lever.anchoredPosition = Vector2.zero;

            UpdatePositionValues();
        }

        private void ProcessInput(Vector2 inputPosition)
        {
            Vector2 tempVector = inputPosition - (Vector2)leverCenter;

            tempVector = Vector2.ClampMagnitude(tempVector, radius);

            lever.transform.position = (Vector2)leverCenter + tempVector;

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
            Vector2 leverPosition = (lever.position - leverCenter) / radius;

            if (GetDistance() <= deadZone)
            {
                leverPosition.x = 0.0f;
                leverPosition.y = 0.0f;
            }

            HorizontalAxis = leverPosition.x;
            VerticalAxis = leverPosition.y;
        }

        private float GetDistance()
        {
            return Vector3.Distance(lever.position, leverCenter) / radius;
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