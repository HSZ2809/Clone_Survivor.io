using UnityEngine;

namespace ZUN
{
    public class DummyManager_Audio : IManager_Audio
    {
        public bool IsBgmOn => true;
        public bool IsSfxOn => true;
        public AudioSource ButtonClickSource => null;
        public void ToggleMusic() { }
        public void ToggleEffectSound() { }
    }
}
