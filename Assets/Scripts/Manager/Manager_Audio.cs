using UnityEngine;
using System;

namespace ZUN
{
    public class Manager_Audio : MonoBehaviour
    {
        public AudioSettings audioSettings;

        public event EventHandler OnToggleMusic;
        public event EventHandler OnToggleEffectSound;

        public void ToggleMusic()
        {
            audioSettings.OnOffMusic();
            
            OnToggleMusic?.Invoke(this, EventArgs.Empty);
        }

        public void ToggleEffectSound()
        {
            audioSettings.OnOffEffectSound();

            OnToggleEffectSound?.Invoke(this, EventArgs.Empty);
        }

        public void ToggleSound()
        {
            audioSettings.OnOffMusic();
            audioSettings.OnOffEffectSound();

            OnToggleMusic?.Invoke(this, EventArgs.Empty);
            OnToggleEffectSound?.Invoke(this, EventArgs.Empty);
        }
    }
}
