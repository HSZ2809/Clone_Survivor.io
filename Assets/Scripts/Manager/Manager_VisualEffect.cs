using UnityEngine;

namespace ZUN
{
    public class Manager_VisualEffect : MonoBehaviour
    {
        readonly string isEffectReducedKey = "isEffectReduced";

        public bool IsEffectReduced { get; private set; }

        private void Start()
        {
            IsEffectReduced = GetBool(isEffectReducedKey);
        }

        void SetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        bool GetBool(string key, bool defaultValue = true)
        {
            int defaultInt = defaultValue ? 1 : 0;
            return PlayerPrefs.GetInt(key, defaultInt) == 1;
        }

        public void ToggleEffectReduction()
        {
            IsEffectReduced = !IsEffectReduced;
            SetBool(isEffectReducedKey, IsEffectReduced);
        }
    }
}