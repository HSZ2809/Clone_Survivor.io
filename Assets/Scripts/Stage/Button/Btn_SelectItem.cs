using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class Btn_SelectItem : MonoBehaviour
    {
        public Manager_Stage manager_Stage = null;
        public TextMeshProUGUI textMeshProUGUI = null;
        public Image image = null;
        public string itemSN = null;

        public void ItemSelect()
        {
            manager_Stage.SelectItem(itemSN);
        }
    }
}