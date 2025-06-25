using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class BossSpawner : MonoBehaviour
    {

        #region Inspector
        [Header("Boss")]
        [SerializeField] BossMonster boss;
        [SerializeField] float hp;
        [SerializeField] float attackPower;

        [Header("UI")]
        [SerializeField] GameObject bossHpUI;
        [SerializeField] Image bossHpBar;
        [SerializeField] TextMeshProUGUI bossName;
        #endregion

        Timer timer;
        BGMCtrl bgmCtrl;
        Animator animator;
        TimeLineCtrl timeLineCtrl;

        private void Awake()
        {
            GameObject chapterCtrl = GameObject.FindGameObjectWithTag("ChapterCtrl");
            if (chapterCtrl != null )
            {
                chapterCtrl.TryGetComponent<Timer>(out timer);
                chapterCtrl.TryGetComponent<BGMCtrl>(out bgmCtrl);
                chapterCtrl.TryGetComponent<TimeLineCtrl>(out timeLineCtrl);
            }
            animator = GetComponent<Animator>();
        }

        public void SetReady()
        {
            timeLineCtrl.Pause();
            transform.parent = null;
            timer.PauseTimer = true;
            bgmCtrl.SetBossClip();
            RemoveMonster();
            bossHpUI.SetActive(true);
            bossName.text = boss.GetBossName();
            bossHpBar.fillAmount = 1;
            animator.SetTrigger("ready");
        }

        private void SetBoss()
        {
            BossMonster mon = Instantiate(boss, transform.position, transform.rotation);
            mon.SetMonsterSpec(hp, attackPower);
            mon.SetBossHpUI(bossHpUI);
            mon.SetBossHpBar(bossHpBar);
            mon.gameObject.SetActive(true);
        }

        private void DestroyObject()
        {
            Destroy(this);
        }

        private void RemoveMonster()
        {
            GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");

            foreach (GameObject monster in monsters)
            {
                monster.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}