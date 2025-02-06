using UnityEngine;

namespace ZUN
{
    public class CarionCrawler : Monster
    {
        [SerializeField] Animator anim;

        [Header("Sprite object")]
        [SerializeField] GameObject sprites;

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
            // 플레이어 방향으로 스프라이트 뒤집기
            // 이동, 패턴 실행
        }

        // 패턴 1: 투사체 발사
        // 패턴 2: 돌진

        public override void Hit(float damage)
        {
            hp -= damage;

            if (hp <= 0)
            {
                cc2D.enabled = false;
                anim.SetTrigger("Die");
            }
        }
    }
}