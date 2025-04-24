using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class ButtonSound : MonoBehaviour
    {
        private void Start()
        {
            if (TryGetComponent<Button>(out var button))
            {
                if (GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_Audio>(out var manager_Audio))
                {
                    button.onClick.AddListener(() =>
                    {
                        manager_Audio.ButtonClickSource.Play();
                    });
                }
                else
                {
                    Debug.LogWarning("Manager_Audio Missing!");
                }
            }
            else
            {
                Debug.LogWarning("Button Component Missing!");
            }
        }
    }
}