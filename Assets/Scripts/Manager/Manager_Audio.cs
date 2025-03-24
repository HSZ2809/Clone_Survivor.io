using UnityEngine;

namespace ZUN
{
    public class Manager_Audio : MonoBehaviour
    {
        [SerializeField] AudioSettings audioSettings;

        //[Space]
        //[SerializeField] AudioSource musicPlayer = new();
        //[SerializeField] AudioSource soundEffectPlayer = new();

        //public AudioSource MusicPlayer { get { return musicPlayer; } }
        //public AudioSource SoundEffectPlayer { get { return soundEffectPlayer; } }

        //private void Start()
        //{
        //    musicPlayer.volume = audioSettings.MusicVolume;
        //    soundEffectPlayer.volume = audioSettings.EffectVolume;
        //}

        public void ToggleMusic()
        {
            audioSettings.ToggleMusic();
            // musicPlayer.volume = audioSettings.MusicVolume;
        }

        public void ToggleEffectSound()
        {
            audioSettings.ToggleEffectSound();
            // soundEffectPlayer.volume = audioSettings.EffectVolume;
        }
    }
}
