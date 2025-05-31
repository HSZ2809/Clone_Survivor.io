using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace ZUN
{
    public class SkillDescriptionCtrl : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI tmp_description;
        [SerializeField] Image lockCheck;

        [SerializeField] Sprite[] lockCheckSprites;
        const int LOCK = 0, UNLOCK = 1;
        readonly Color gray = new(0.5f, 0.5f, 0.5f);

        public void SetDescription(string description)
        {
            tmp_description.text = description;
        }

        public void Unlock()
        {
            lockCheck.sprite = lockCheckSprites[UNLOCK];
            tmp_description.color = Color.white;
        }

        private void OnDisable()
        {
            lockCheck.sprite = lockCheckSprites[LOCK];
            tmp_description.color = gray;
        }
    }
}