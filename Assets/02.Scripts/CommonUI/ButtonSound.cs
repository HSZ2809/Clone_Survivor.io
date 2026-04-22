using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ZUN
{
    public class ButtonSound : MonoBehaviour
    {
        [Inject] private IManager_Audio manager_Audio;

        private void Start()
        {
            if (TryGetComponent<Button>(out var button))
            {
                button.onClick.AddListener(() =>
                {
                    manager_Audio.ButtonClickSource.Play();
                });
            }
            else
            {
                Debug.LogWarning("Button Component Missing!");
            }
        }
    }
}