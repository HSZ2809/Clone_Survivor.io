using UnityEngine;

namespace ZUN
{
    public interface IManager_Vibration
    {
        bool IsVibrationEnabled { get; }
        void TriggerVibration();
        void ToggleVibration();
    }
}