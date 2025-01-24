using TMPro;
using UnityEngine;

namespace ZUN
{
    public class TimerUI : MonoBehaviour
    {
        StageCtrl stageCtrl;
        [SerializeField] TMP_Text text_time;

        int minute = 0;
        int second = 0;

        private void Awake()
        {
            stageCtrl = GameObject.FindGameObjectWithTag("StageCtrl").GetComponent<StageCtrl>();
        }

        private void FixedUpdate()
        {
            minute = Mathf.FloorToInt(stageCtrl.PlayTime / 60);
            second = Mathf.FloorToInt(stageCtrl.PlayTime % 60);

            text_time.text = string.Format("{0:00} : {1:00}", minute, second);
        }
    }
}