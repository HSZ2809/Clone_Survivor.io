using UnityEngine;

namespace ZUN
{
    [CreateAssetMenu(fileName = "AudioSettings", menuName = "Scriptable Objects/AudioSettings")]
    public class AudioSettings : ScriptableObject
    {
        private bool isMusicMute;
        private bool isEffectSoundMute;

        public float MusicVolume { get { return (isMusicMute) ? -80.0f : 0.0f; } }
        public float EffectVolume { get { return (isEffectSoundMute) ? -80.0f : 0.0f; } }

        public void ToggleMusic()
        {
            isMusicMute = !isMusicMute;
        }

        public void ToggleEffectSound()
        {
            isEffectSoundMute = !isEffectSoundMute;
        }
    }
}
