using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class Bouncebloom : Monster
    {
        [SerializeField] Animator anim;

        [Header("Sprite Renderer")]
        [SerializeField] SpriteRenderer sr;

        [Header("Treasure Box")]
        [SerializeField] TreasureBox treasureBox;

        [Header("Boss Pattern")]
        [SerializeField] MonsterBullet bullet;
        WaitForSeconds waitTime = new(5);

        private void Awake()
        {
            tag = "Monster";
            gameObject.layer = LayerMask.NameToLayer(tag);
            cc2D = GetComponent<CircleCollider2D>();
            characterTF = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>().transform;
            chapterCtrl = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<ChapterCtrl>();
        }

        private void Update()
        {
            if (characterTF.position.x > transform.position.x)
                sr.flipX = false;
            else
                sr.flipX = true;
        }

        private void OnEnable()
        {
            hp = maxHp;
            StartCoroutine(Pattern_Shoot());
        }

        IEnumerator Pattern_Shoot()
        {
            yield return waitTime;

            Vector3 aim = (transform.position - characterTF.position).normalized;
            float angle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
            bullet.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, angle + 90));
            bullet.gameObject.SetActive(true);
        }

        public override void Hit(float damage)
        {
            hp -= damage;

            if (hp <= 0)
            {
                cc2D.enabled = false;
                StopCoroutine(Pattern_Shoot());
                anim.SetTrigger("Die");
            }
        }

        public void DropTreasureBox()
        {
            Instantiate(treasureBox, transform.position, transform.rotation);
            chapterCtrl.AddKillCount();
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}