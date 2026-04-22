using UnityEngine;

namespace ZUN
{
    public class Manager_Alert : MonoBehaviour, IManager_Alert
    {
        [SerializeField] Canvas canvas;
        [SerializeField] AlertPopup prefab;

        public void ShowPopup(string message)
        {
            AlertPopup popup = Instantiate(prefab, canvas.transform, false);
            popup.SetMessage(message);
        }
    }
}