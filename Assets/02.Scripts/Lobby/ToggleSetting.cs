using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class ToggleSetting : MonoBehaviour
    {
        [SerializeField] Image settingImage;
        [SerializeField] Image toggleImage;
        [SerializeField] Button button;

        [Space]
        [SerializeField] Sprite sprite_settingOn;
        [SerializeField] Sprite sprite_settingOff;
        [SerializeField] Sprite sprite_toggleOn;
        [SerializeField] Sprite sprite_toggleOff;

        bool isStateOn;

        public Button Button => button;

        void Awake()
        {
            if (settingImage == null || toggleImage == null)
                Debug.LogWarning("Image not found!");

            if (button == null)
                Debug.LogWarning("Button not found!");
            else
            {
                button.onClick.AddListener(() =>
                {
                    ChangeSpriteOnClick();
                });
            }
        }

        void ChangeSpriteOnClick()
        {
            if (settingImage == null || toggleImage == null)
            {
                Debug.LogWarning("Image not found!");
                return;
            }

            if (isStateOn)
            {
                settingImage.sprite = sprite_settingOff;
                toggleImage.sprite = sprite_toggleOff;
            }
            else
            {
                settingImage.sprite = sprite_settingOn;
                toggleImage.sprite = sprite_toggleOn;
            }

            isStateOn = !isStateOn;
        }

        public void InitializeButton(bool state)
        {
            if (settingImage == null || toggleImage == null)
            {
                Debug.LogWarning("Image not found!");
                return;
            }

            if (state)
            {
                settingImage.sprite = sprite_settingOn;
                toggleImage.sprite = sprite_toggleOn;
            }
            else
            {
                settingImage.sprite = sprite_settingOff;
                toggleImage.sprite = sprite_toggleOff;
            }

            isStateOn = state;
        }
    }
}