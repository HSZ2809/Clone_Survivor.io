using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class Btn_SelectSkill : MonoBehaviour
    {
        public StageCtrl stageCtrl;

        public TextMeshProUGUI id;
        public Image image;
        public TextMeshProUGUI upgradeInfo;

        public void Select()
        {
            stageCtrl.SelectSkill(id.text);
        }
    }
}