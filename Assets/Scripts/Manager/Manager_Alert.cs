using UnityEngine;

namespace ZUN
{
    public class Manager_Alert : MonoBehaviour
    {
        [SerializeField] Canvas canvas;
        [SerializeField] AlertPopup prefab;

        public void GetPopup(string message)
        {
            AlertPopup popup = Instantiate(prefab, canvas.transform, false);
            popup.SetMessage(message);
        }
    }
}