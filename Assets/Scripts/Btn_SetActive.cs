using UnityEngine;

namespace ZUN
{
    public class Btn_SetActive : MonoBehaviour
    {
        public GameObject target;

        public void SetActiveTrue()
        {
            target.SetActive(true);
        }

        public void SetActiveFalse()
        {
            target.SetActive(false);
        }
    }
}