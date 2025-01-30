using TMPro;
using UnityEngine;

namespace ZUN
{
    public class TimerUI : MonoBehaviour
    {
        ChapterCtrl chapterCtrl;
        [SerializeField] TMP_Text text_time;

        int minute = 0;
        int second = 0;

        private void Awake()
        {
            chapterCtrl = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<ChapterCtrl>();
        }

        private void FixedUpdate()
        {
            minute = Mathf.FloorToInt(chapterCtrl.PlayTime / 60);
            second = Mathf.FloorToInt(chapterCtrl.PlayTime % 60);

            text_time.text = string.Format("{0:00} : {1:00}", minute, second);
        }
    }
}