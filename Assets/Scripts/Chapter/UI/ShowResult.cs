using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ZUN
{
    public class ShowResult : MonoBehaviour
    {
        #region Inspector
        [SerializeField] Image img_Title;
        [SerializeField] GameObject record;
        [SerializeField] TextMeshProUGUI txt_PlayTime;
        [SerializeField] TextMeshProUGUI txt_TimeRecord;
        [SerializeField] TextMeshProUGUI txt_KillCount;
        [SerializeField] GameObject reward;
        [SerializeField] GameObject wishingBottle;
        [SerializeField] Button btn_Confirm;
        #endregion

        ChapterCtrl chapterCtrl;
        Timer timer;

        private void Awake()
        {
            chapterCtrl = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<ChapterCtrl>();
            timer = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<Timer>();
        }

        private void Start()
        {
            btn_Confirm.onClick.AddListener(() =>
            {
                Manager_Scene manager_Scene;
                manager_Scene = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager_Scene>();
                manager_Scene.LoadScene("Lobby", gameObject.scene.buildIndex);
            });
        }

        private void OnEnable()
        {
            if (timer.PlayTime > 900.0f)
            {
                txt_PlayTime.text = string.Format("15 : 00");
            }
            else
            {
                int minute = Mathf.FloorToInt(timer.PlayTime / 60);
                int second = Mathf.FloorToInt(timer.PlayTime % 60);

                txt_PlayTime.text = string.Format("{0:00} : {1:00}", minute, second);
            }

            // 임시 코드
            if (timer.PlayTime > 900.0f)
            {
                txt_TimeRecord.text = string.Format("15 : 00");
            }
            else
            {
                int minute = Mathf.FloorToInt(timer.PlayTime / 60);
                int second = Mathf.FloorToInt(timer.PlayTime % 60);

                txt_TimeRecord.text = string.Format("{0:00} : {1:00}", minute, second);
            }

            txt_KillCount.text = chapterCtrl.KillCount.ToString();
        }
    }
}