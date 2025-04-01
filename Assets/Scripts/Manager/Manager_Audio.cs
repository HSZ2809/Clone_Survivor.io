using UnityEngine;
using UnityEngine.Audio;

namespace ZUN
{
    public class Manager_Audio : MonoBehaviour
    {
        [SerializeField] AudioSettings audioSettings;
        [SerializeField] AudioMixer audioMixer;

        private void Start()
        {
            audioMixer.SetFloat("BGM", audioSettings.MusicVolume);
            audioMixer.SetFloat("SFX", audioSettings.EffectVolume);
        }

        public void ToggleMusic()
        {
            audioSettings.ToggleMusic();
            audioMixer.SetFloat("BGM", audioSettings.MusicVolume);
        }

        public void ToggleEffectSound()
        {
            audioSettings.ToggleEffectSound();
            audioMixer.SetFloat("SFX", audioSettings.EffectVolume);
        }
    }
}
