using TMPro;
using UnityEngine;

namespace ZUN
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] TMP_Text text_time;
        int minute = 0;
        float second = 0;
        public int GameTime { get => minute * 60 + (int)second; }

        private void FixedUpdate()
        {
            second += Time.deltaTime;

            if (second >= 60)
            {
                minute += 1;
                second = 0;
            }

            text_time.text = string.Format("{0:D2} : {1:D2}", minute, (int)second);
        }
    }
}