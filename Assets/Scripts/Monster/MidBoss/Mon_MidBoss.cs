using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.U2D.IK;

namespace ZUN
{
    public class Mon_MidBoss : Monster, IMon_Movement, IMon_Damageable, IMon_Attackable, IMon_Destroyable
    {
        #region Inspector
        [Header("Status")]
        [SerializeField] float hp;
        [SerializeField] float ap;
        [SerializeField] float moveSpeed;

        [SerializeField] Animator anim;

        [Header("Sprite Renderer")]
        [SerializeField] private SpriteRenderer sr;

        [Header("Treasure Box")]
        [SerializeField] private TreasureBox treasureBox;
        #endregion

        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public float MaxHp { get; set; }
        public float Ap { get => ap; set => ap = value; }

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
            if (character.transform.position.x > transform.position.x)
                sr.flipX = false;
            else
                sr.flipX = true;

            if (hp > 0)
                Move();
        }

        public override void SetMonsterSpec(float _maxHp, float _ap)
        {
            MaxHp = _maxHp;
            ap = _ap;
        }
        public void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, character.transform.position, Time.deltaTime * moveSpeed);
        }

        public void TakeDamage(float damage)
        {
            hp -= damage;
            ShowDamage(damage);

            if (hp <= 0)
            {
                cc2D.enabled = false;
                anim.SetTrigger("Die");
            }
        }

        public void ShowDamage(float damage)
        {
            // 데미지 표시 로직
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