namespace ZUN
{
    public class DummyManager_VisualEffect : IManager_VisualEffect
    {
        public bool IsEffectReduced => false;
        public void ToggleEffectReduction() { }
    }
}
