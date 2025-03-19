using UnityEngine;

namespace ZUN
{
    [CreateAssetMenu(fileName = "AudioSettings", menuName = "Scriptable Objects/AudioSettings")]
    public class AudioSettings : ScriptableObject
    {
        private bool isMusicMute;
        private bool isEffeckSoundMute;

        public float MusicVolume { get { return (isMusicMute) ? 0.0f : 1.0f; } }
        public float EffectVolume { get { return (isEffeckSoundMute) ? 0.0f : 1.0f; } }

        public void ToggleMusic()
        {
            isMusicMute = !isMusicMute;
        }

        public void ToggleEffectSound()
        {
            isEffeckSoundMute = !isEffeckSoundMute;
        }
    }
}
