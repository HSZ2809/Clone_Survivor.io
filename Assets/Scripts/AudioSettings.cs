using UnityEngine;
using UnityEngine.Rendering;

namespace ZUN
{
    [CreateAssetMenu(fileName = "AudioSettings", menuName = "Scriptable Objects/AudioSettings")]
    public class AudioSettings : ScriptableObject
    {
        private bool isMusicMute;
        private bool isEffeckSoundMute;

        public float MusicVolume { get { (isMusicMute) ? 0.0f : 1.0f; } }
        public float EffectVolume { get { (isEffectSoundMute) ? 0.0f : 1.0f; } }

        public void OnOffMusic()
        {
            isMusicMute = !isMusicMute;
        }

        public void OnOffEffectSound()
        {
            isEffeckSoundMute = !isEffeckSoundMute;
        }
    }
}
