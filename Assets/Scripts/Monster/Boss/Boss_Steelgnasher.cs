using UnityEngine;

namespace ZUN
{
    public class Boss_Steelgnasher : Monster, IMon_Movement, IMon_Damageable, IMon_Attackable, IMon_Destroyable
    {
        #region Inspector
        [SerializeField] float hp;
        [SerializeField] float ap;
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

        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public float MaxHp { get; set; }
        public float Ap { get => ap; set => ap = value; }

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
                    Move();
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Character"))
            {
                character.TakeDamage(ap);
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

        public override void SetMonsterSpec(float _maxHp, float _ap)
        {
            MaxHp = _maxHp;
            Ap = _ap;

            hp = MaxHp;
            ap = Ap;
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

        private void ShootBullet()
        {
            
        }

        private void DropTreasureBox()
        {
            Instantiate(treasureBox, transform.position, transform.rotation);
            chapterCtrl.AddKillCount();
        }

        public void Die()
        {
            chapterCtrl.PauseTimer = false;
            Destroy(gameObject);
        }
    }
}