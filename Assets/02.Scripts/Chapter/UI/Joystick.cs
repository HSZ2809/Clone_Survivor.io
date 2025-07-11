using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ZUN
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform lever;
        [SerializeField] private RectTransform stickBase;

        [Space]
        [SerializeField] private Image[] joystickImages;

        Manager_JoystickSetting manager_Joystick;
        Vector2 baseOriginalLocation;
        Vector2 leverCenter;
        float radius;
        readonly float joystickSize = 25.0f;

        private void Awake()
        {
            manager_Joystick = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager_JoystickSetting>();
        }

        private void Start()
        {
            baseOriginalLocation = stickBase.position;
            leverCenter = stickBase.position;
            radius = Screen.height / joystickSize;

            foreach (var image in joystickImages)
                if (!manager_Joystick.IsJoystickVisible)
                    image.enabled = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            LocationSet(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            ProcessInput(eventData.position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            BackOriginalLocation();
            UpdatePositionValues();
        }

        private void LocationSet(Vector2 eventData)
        {
            stickBase.transform.position = eventData;
            leverCenter = eventData;
        }

        private void BackOriginalLocation()
        {
            stickBase.transform.position = baseOriginalLocation;
            lever.transform.position = baseOriginalLocation;
            leverCenter = baseOriginalLocation;
        }

        private void ProcessInput(Vector2 inputPosition)
        {
            Vector2 tempVector = inputPosition - leverCenter;

            tempVector = Vector2.ClampMagnitude(tempVector, radius);

            lever.transform.position = leverCenter + tempVector;

            UpdatePositionValues();
        }

        private void UpdatePositionValues()
        {
            Vector2 leverPosition = ((Vector2)lever.position - leverCenter) / radius;

            HorizontalAxis = leverPosition.x;
            VerticalAxis = leverPosition.y;
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