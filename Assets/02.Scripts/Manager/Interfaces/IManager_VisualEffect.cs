using UnityEngine;

namespace ZUN
{
    public interface IManager_VisualEffect
    {
        bool IsEffectReduced { get; }
        void ToggleEffectReduction();
    }
}