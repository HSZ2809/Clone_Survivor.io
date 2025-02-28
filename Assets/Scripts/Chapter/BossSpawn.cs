using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class BossSpawn : MonoBehaviour
    {
        //Character character;

        [Header("Boss")]
        [SerializeField] BossMonster boss;
        [SerializeField] float hp;
        [SerializeField] float attackPower;

        [Header("UI")]
        [SerializeField] GameObject bossHpUI;
        [SerializeField] Image bossHpBar;
        [SerializeField] TextMeshProUGUI bossName;

        //[Header("Range")]
        //[SerializeField] float disistance = 16.45f;

        Animator animator;

        private void Awake()
        {
            //character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            animator = GetComponent<Animator>();
        }

        public void SetReady()
        {
            transform.parent = null;

            RemoveMonster();
            bossHpUI.SetActive(true);
            bossName.text = boss.GetBossName();
            bossHpBar.fillAmount = 1;
            animator.SetTrigger("ready");
        }

        private void SetBoss()
        {
            // Vector2 setLoc = character.transform.position + new Vector3(0, disistance);
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