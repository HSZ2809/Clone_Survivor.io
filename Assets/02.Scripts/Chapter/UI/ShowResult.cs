using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ZUN
{
    public class ShowResult : MonoBehaviour
    {
        #region Inspector
        [SerializeField] Image img_Victory;
        [SerializeField] Image img_Defeat;
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
            Time.timeScale = 0.0f;

            btn_Confirm.onClick.AddListener(() =>
            {
                if (GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_Scene>(out var manager_Scene))
                    manager_Scene.LoadScene("Lobby", gameObject.scene.buildIndex);
                else
                    Debug.LogWarning("Manager_Scene not found");
            });
        }

        private void OnEnable()
        {
            if (timer.PlayTime > 900.0f)
            {
                img_Defeat.gameObject.SetActive(false);
                txt_PlayTime.text = string.Format("15 : 00");
            }
            else
            {
                img_Victory.gameObject.SetActive(false);
                int minute = Mathf.FloorToInt(timer.PlayTime / 60);
                int second = Mathf.FloorToInt(timer.PlayTime % 60);

                txt_PlayTime.text = string.Format("{0:00} : {1:00}", minute, second);
            }

            
            //if (timer.PlayTime > 900.0f)
            //{
            //    txt_TimeRecord.text = string.Format("15 : 00");
            //}
            //else
            //{
            //    int minute = Mathf.FloorToInt(timer.PlayTime / 60);
            //    int second = Mathf.FloorToInt(timer.PlayTime % 60);

            //    txt_TimeRecord.text = string.Format("{0:00} : {1:00}", minute, second);
            //}

            txt_KillCount.text = chapterCtrl.KillCount.ToString();
        }
    }
}