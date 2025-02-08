using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.U2D.IK;

namespace ZUN
{
    public class Boss_Steelgnasher : Monster
    {
        #region Inspector
        [SerializeField] float moveSpeed;

        [SerializeField] Animator anim;

        [Header("Sprite object")]
        [SerializeField] GameObject sprites;

        [Header("Treasure Box")]
        [SerializeField] TreasureBox treasureBox;

        [Header("Boss Pattern")]
        [SerializeField] Transform shootPos;
        [SerializeField] MonsterBullet bullet;
        #endregion

        readonly Vector2 originX = new(1, 1);
        readonly Vector2 flipX = new(-1, 1);

        bool isIdle = true;

        private void Awake()
        {
            tag = "Monster";
            gameObject.layer = LayerMask.NameToLayer(tag);
            cc2D = GetComponent<CircleCollider2D>();
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            chapterCtrl = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<ChapterCtrl>();
        }

        private void Update()
        {
            if (isIdle)
            {
                if (character.transform.position.x > transform.position.x)
                    sprites.transform.localScale = flipX;
                else
                    sprites.transform.localScale = originX;

                if (hp > 0)
                    transform.position = Vector3.MoveTowards(transform.position, character.transform.position, Time.deltaTime * moveSpeed);
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Character"))
            {
                character.Hit(attackPower);
            }
        }

        private void SetIsIdleFalse()
        {
            isIdle = false;
        }

        private void SetIsIdleTrue()
        {
            isIdle = true;
        }

        public override void Hit(float damage)
        {
            hp -= damage;

            if (hp <= 0)
            {
                cc2D.enabled = false;
                anim.SetTrigger("Die");
            }
        }

        private void ShootBullet()
        {
            
        }

        private void DropTreasureBox()
        {
            Instantiate(treasureBox, transform.position, transform.rotation);
            chapterCtrl.AddKillCount();
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}