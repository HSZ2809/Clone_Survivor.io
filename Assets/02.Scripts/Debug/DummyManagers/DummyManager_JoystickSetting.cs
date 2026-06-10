namespace ZUN
{
    public class DummyManager_JoystickSetting : IManager_JoystickSetting
    {
        public bool IsJoystickVisible => true;
        public void ToggleJoystickVisible() { }
    }
}
