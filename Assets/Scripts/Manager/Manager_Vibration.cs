using UnityEngine;

namespace ZUN
{
    public class Manager_Vibration : MonoBehaviour
    {
        readonly string isVibrationEnabledKey = "isVibrationEnabled";

        public bool IsVibrationEnabled { get; private set; }

        private void Start()
        {
            IsVibrationEnabled = GetBool(isVibrationEnabledKey);
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

        public void TriggerVibration()
        {
            if (Application.isMobilePlatform)
            {
                if (IsVibrationEnabled)
                    Handheld.Vibrate();
            }
        }

        public void ToggleVibration()
        {
            if (IsVibrationEnabled)
                SetBool(isVibrationEnabledKey, false);
            else
                SetBool(isVibrationEnabledKey, true);

            IsVibrationEnabled = !IsVibrationEnabled;
        }
    }
}