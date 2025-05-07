using UnityEngine;

namespace ZUN
{
    public class SettingsCtrl : MonoBehaviour
    {
        enum settingName
        {
            SFX = 0,
            BGM,
            VIBRATION,
            EFFECT,
            JOYSTICK
        }

        [SerializeField] ToggleSetting[] toggleSettings = new ToggleSetting[5];
        Manager_Audio manager_Audio;

        private void Awake()
        {
            manager_Audio = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager_Audio>();
        }

        private void Start()
        {
            toggleSettings[(int)settingName.SFX].InitializeButton(manager_Audio.AudioSettings.IsEffectSoundMute);
            toggleSettings[(int)settingName.BGM].InitializeButton(manager_Audio.AudioSettings.IsMusicMute);
        }
    }
}