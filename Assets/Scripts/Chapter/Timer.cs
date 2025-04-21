using TMPro;
using UnityEngine;

namespace ZUN
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] TMP_Text text_time;
        public float PlayTime { get; private set; }
        public bool PauseTimer { get; set; }

        int minute = 0;
        int second = 0;

        private void FixedUpdate()
        {
            if (!PauseTimer)
            {
                PlayTime += Time.deltaTime;

                minute = Mathf.FloorToInt(PlayTime / 60);
                second = Mathf.FloorToInt(PlayTime % 60);

                text_time.text = string.Format("{0:00} : {1:00}", minute, second);
            }
        }
    }
}