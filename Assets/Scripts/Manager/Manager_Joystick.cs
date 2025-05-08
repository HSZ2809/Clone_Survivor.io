using UnityEngine;

namespace ZUN
{
    public class Manager_Joystick : MonoBehaviour
    {
        readonly string isJoystickVisibleKey = "isJoystickVisible";

        public bool IsJoystickVisible { get; private set; }

        private void Start()
        {
            IsJoystickVisible = GetBool(isJoystickVisibleKey);
        }

        void SetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        bool GetBool(string key, bool defaultValue = true)
        {
            int defaultInt = defaultValue ? 1 : 0;
            return PlayerPrefs.GetInt(key, defaultInt) == 1;
        }

        public void ToggleJoystickVisible()
        {
            IsJoystickVisible = !IsJoystickVisible;
            SetBool(isJoystickVisibleKey, IsJoystickVisible);
        }
    }
}