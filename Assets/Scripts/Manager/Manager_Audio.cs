using UnityEngine;
using UnityEngine.Audio;

namespace ZUN
{
    public class Manager_Audio : MonoBehaviour
    {
        readonly string isBgmOnKey = "isBgmOn";
        readonly string isSfxOnKey = "isSfxOn";

        public bool IsBgmOn { get; private set; }
        public bool IsSfxOn { get; private set; }

        [SerializeField] AudioMixer audioMixer;

        [Space]
        [SerializeField] AudioSource buttonClickSource;

        public AudioSource ButtonClickSource => buttonClickSource;

        private void Start()
        {
            IsBgmOn = GetBool(isBgmOnKey);
            IsSfxOn = GetBool(isSfxOnKey);

            float MusicVolume = IsBgmOn ? 0.0f : -80.0f;
            float EffectVolume = IsSfxOn ? 0.0f : -80.0f;

            audioMixer.SetFloat("BGM", MusicVolume);
            audioMixer.SetFloat("SFX", EffectVolume);
        }

        void SetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        bool GetBool(string key, bool defaultValue = false)
        {
            int defaultInt = defaultValue ? 1 : 0;
            return PlayerPrefs.GetInt(key, defaultInt) == 1;
        }

        public void ToggleMusic()
        {
            float MusicVolume;

            if (IsBgmOn)
            {
                MusicVolume = -80.0f;
                audioMixer.SetFloat("BGM", MusicVolume);
                SetBool(isBgmOnKey, false);
            }
            else
            {
                MusicVolume = 0.0f;
                audioMixer.SetFloat("BGM", MusicVolume);
                SetBool(isBgmOnKey, true);
            }

            IsBgmOn = !IsBgmOn;
        }

        public void ToggleEffectSound()
        {
            float EffectVolume;

            if (IsSfxOn)
            {
                EffectVolume = -80.0f;
                audioMixer.SetFloat("SFX", EffectVolume);
                SetBool(isSfxOnKey, false);
            }
            else
            {
                EffectVolume = 0.0f;
                audioMixer.SetFloat("SFX", EffectVolume);
                SetBool(isSfxOnKey, true);
            }

            IsSfxOn = !IsSfxOn;
        }
    }
}