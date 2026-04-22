using UnityEngine;

namespace ZUN
{
    public interface IManager_JoystickSetting
    {
        bool IsJoystickVisible { get; }
        void ToggleJoystickVisible();
    }
}