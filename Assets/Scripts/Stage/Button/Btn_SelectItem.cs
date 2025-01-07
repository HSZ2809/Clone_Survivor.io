using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class Btn_SelectItem : MonoBehaviour
    {
        public Manager_Stage staff_Play = null;
        public TextMeshProUGUI textMeshProUGUI = null;
        public Image image = null;
        public string itemSN = null;

        public void ItemSelect()
        {
            staff_Play.SelectItem(itemSN);
        }
    }
}