using UnityEngine;
using UnityEngine.Audio;

namespace ZUN
{
    public class Manager_Audio : MonoBehaviour, IManager_Audio
    {
        readonly string isBgmOnKey = "isBgmOn";
        readonly string isSfxOnKey = "isSfxOn";
        bool _isBgmOn;
        bool _isSfxOn;

        public bool IsBgmOn { get { return _isBgmOn; } }
        public bool IsSfxOn { get { return _isSfxOn; } }

        [SerializeField] AudioMixer audioMixer;

        [Space]
        [SerializeField] AudioSource buttonClickSource;

        public AudioSource ButtonClickSource => buttonClickSource;

        private void Start()
        {
            _isBgmOn = GetBool(isBgmOnKey);
            _isSfxOn = GetBool(isSfxOnKey);

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

            _isBgmOn = !_isBgmOn;
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

            _isSfxOn = !_isSfxOn;
        }
    }
}