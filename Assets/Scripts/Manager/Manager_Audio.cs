using UnityEngine;

namespace ZUN
{
    public class Manager_Audio : MonoBehaviour
    {
        public AudioSettings audioSettings;

        public void SetMusicOnOff()
        {
            audioSettings.OnOffMusic();
        }

        public void SetEffectSoundOnOff()
        {
            audioSettings.OnOffEffectSound();
        }
    }
}