using TMPro;
using UnityEngine;

namespace ZUN
{
    public class TimerUI : MonoBehaviour
    {
        Manager_Stage manager_Stage;
        [SerializeField] TMP_Text text_time;

        int minute = 0;
        int second = 0;

        private void Awake()
        {
            manager_Stage = GameObject.FindGameObjectWithTag("Manager_Stage").GetComponent<Manager_Stage>();
        }

        private void FixedUpdate()
        {
            minute = Mathf.FloorToInt(manager_Stage.PlayTime / 60);
            second = Mathf.FloorToInt(manager_Stage.PlayTime % 60);

            text_time.text = string.Format("{0:00} : {1:00}", minute, second);
        }
    }
}