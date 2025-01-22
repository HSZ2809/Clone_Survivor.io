using UnityEngine;
using UnityEngine.Rendering;

namespace ZUN
{
    [CreateAssetMenu(fileName = "AudioSettings", menuName = "Scriptable Objects/AudioSettings")]
    public class AudioSettings : ScriptableObject
    {
        private float musicVolume;
        private float effectVolume;

        public float MusicVolume { get { return musicVolume; } }
        public float EffectVolume { get { return effectVolume; } }

        public void OnOffMusic()
        {
            if (musicVolume >= 1.0f)
                musicVolume = 0.0f;
            else
                musicVolume = 1.0f;
        }

        public void OnOffEffectSound()
        {
            if (effectVolume >= 1.0f)
                effectVolume = 0.0f;
            else
                effectVolume = 1.0f;
        }
    }
}