using UnityEngine;
using Zenject;

namespace ZUN
{
    public class SettingsCtrl : MonoBehaviour
    {
        #region Inspector
        [SerializeField] ToggleSetting sfx;
        [SerializeField] ToggleSetting bgm;
        [SerializeField] ToggleSetting vibration;
        [SerializeField] ToggleSetting visualEffect;
        [SerializeField] ToggleSetting showJoystick;
        #endregion

        [Inject] IManager_Audio manager_Audio;
        [Inject] IManager_Vibration manager_Vibration;
        [Inject] IManager_VisualEffect manager_VisualEffect;
        [Inject] IManager_JoystickSetting manager_Joystick;

        private void Start()
        {
            sfx.InitializeButton(manager_Audio.IsSfxOn);
            sfx.Button.onClick.AddListener(() => manager_Audio.ToggleEffectSound());

            bgm.InitializeButton(manager_Audio.IsBgmOn);
            bgm.Button.onClick.AddListener(() => manager_Audio.ToggleMusic());

            vibration.InitializeButton(manager_Vibration.IsVibrationEnabled);
            vibration.Button.onClick.AddListener(() => manager_Vibration.ToggleVibration());

            visualEffect.InitializeButton(manager_VisualEffect.IsEffectReduced);
            visualEffect.Button.onClick.AddListener(() => manager_VisualEffect.ToggleEffectReduction());

            showJoystick.InitializeButton(manager_Joystick.IsJoystickVisible);
            showJoystick.Button.onClick.AddListener(() => manager_Joystick.ToggleJoystickVisible());
        }
    }
}