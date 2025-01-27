using TMPro;
using UnityEngine;

namespace ZUN
{
    public class TimerUI : MonoBehaviour
    {
        MissionCtrl missionCtrl;
        [SerializeField] TMP_Text text_time;

        int minute = 0;
        int second = 0;

        private void Awake()
        {
            missionCtrl = GameObject.FindGameObjectWithTag("MissionCtrl").GetComponent<MissionCtrl>();
        }

        private void FixedUpdate()
        {
            minute = Mathf.FloorToInt(missionCtrl.PlayTime / 60);
            second = Mathf.FloorToInt(missionCtrl.PlayTime % 60);

            text_time.text = string.Format("{0:00} : {1:00}", minute, second);
        }
    }
}