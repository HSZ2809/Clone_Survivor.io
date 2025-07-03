using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class CharacterDisplay : MonoBehaviour
    {
        [SerializeField] Image img_weapon;

        // Animator animator;

        public void SetWeapon(Sprite sprite)
        {
            if (sprite == null)
                img_weapon.enabled = false;
            else
            {
                img_weapon.enabled = true;
                img_weapon.sprite = sprite;
            }
        }
    }
}