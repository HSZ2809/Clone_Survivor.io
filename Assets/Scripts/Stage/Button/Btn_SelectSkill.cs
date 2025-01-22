using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class Btn_SelectSkill : MonoBehaviour
    {
        public Manager_Stage manager_Stage;

        public TextMeshProUGUI id;
        public Image image;
        public TextMeshProUGUI upgradeInfo;

        public void Select()
        {
            manager_Stage.SelectSkill(id.text);
        }
    }
}