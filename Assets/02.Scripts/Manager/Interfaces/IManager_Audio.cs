using System;
using UnityEngine;

namespace ZUN
{
    public interface IManager_Audio
    {
        bool IsBgmOn { get; }
        bool IsSfxOn { get; }

        AudioSource ButtonClickSource { get; }
        
        void ToggleMusic();
        void ToggleEffectSound();
    }
}