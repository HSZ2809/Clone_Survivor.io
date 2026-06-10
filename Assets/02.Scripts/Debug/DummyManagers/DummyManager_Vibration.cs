namespace ZUN
{
    public class DummyManager_Vibration : IManager_Vibration
    {
        public bool IsVibrationEnabled => false;
        public void TriggerVibration() { }
        public void ToggleVibration() { }
    }
}
