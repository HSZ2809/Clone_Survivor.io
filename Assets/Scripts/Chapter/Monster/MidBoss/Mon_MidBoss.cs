using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.U2D.IK;

namespace ZUN
{
    public class Mon_MidBoss : Monster
    {
        [SerializeField] Animator anim;

        [Header("Sprite Renderer")]
        [SerializeField] private SpriteRenderer sr;

        [Header("Treasure Box")]
        [SerializeField] private TreasureBox treasureBox;

        private void Awake()
        {
            tag = "Monster";
            gameObject.layer = LayerMask.NameToLayer(tag);
            cc2D = GetComponent<CircleCollider2D>();
            characterTF = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>().transform;
            chapterCtrl = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<ChapterCtrl>();
        }

        private void OnEnable()
        {
            hp = maxHp;
        }

        private void Update()
        {
            if (characterTF.position.x > transform.position.x)
                sr.flipX = false;
            else
                sr.flipX = true;

            if (hp > 0)
                transform.position = Vector3.MoveTowards(transform.position, characterTF.position, Time.deltaTime * moveSpeed);
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