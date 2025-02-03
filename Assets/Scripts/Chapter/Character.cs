using System;
using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class Character : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] Transform moveDirection = null;
        [SerializeField] SpriteRenderer dirArrow;
        [SerializeField] Image expBar;
        [SerializeField] Image hpBar;
        [SerializeField] Image reloadBar;

        [Header("Skill")]
        [SerializeField] ActiveSkill[] actives = new ActiveSkill[6];
        [SerializeField] PassiveSkill[] passives = new PassiveSkill[6];

        ChapterCtrl chapterCtrl;
        Manager_Inventory inventory;

        [Header("Connected Joystick")]
        [SerializeField] Joystick joystick;

        [Header("Status")]
        [SerializeField] int level;
        [SerializeField] float maxExp;
        [SerializeField] float exp;
        [SerializeField] float maxHp;
        [SerializeField] float hp;
        [SerializeField] float atk;
        [SerializeField] float atkSpeed;
        [SerializeField] float bulletSpeed;
        [SerializeField] float range;
        [SerializeField] float def;
        [SerializeField] float moveSpeed;
        [SerializeField] float expGain;
        [SerializeField] float goldGain;
        [SerializeField] float regeneration;
        [SerializeField] float duration;
        [SerializeField] float itemRange;

        public int AmountOfActive { get; private set; }
        public int AmountOfPassive { get; private set; }

        public float MaxHp { get { return maxHp; } }
        public float Hp 
        { 
            get { return hp; }
            set 
            {
                hp = value;

                if(hp > maxHp)
                    hp = maxHp;
            } 
        }
        public float Atk { get { return atk; } }
        public float AtkSpeed { get { return atkSpeed; } }
        public float BulletSpeed { get { return bulletSpeed; } }
        public float MoveSpeed { get { return moveSpeed; } }
        public float ExpGain { get { return expGain; } }
        public float Duration { get { return duration; } }
        public Transform GetMoveDir() { return moveDirection; }
        public Image ReloadBar() { return reloadBar; }

        public ActiveSkill[] Actives { get { return actives; } }
        public PassiveSkill[] Passives { get { return passives; } }

        private void Awake()
        {
            chapterCtrl = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<ChapterCtrl>();
            inventory = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager_Inventory>();
        }

        private void Update()
        {
            float h = joystick.GetHorizontalAxis();
            float v = joystick.GetVerticalAxis();

            Vector3 direction = new Vector3(h, v, 0).normalized;

            transform.Translate(Time.deltaTime * moveSpeed * direction);

            if (h != 0.0f || v != 0.0f)
            {
                dirArrow.gameObject.SetActive(true);
                float angle = Mathf.Atan2(v, h) * Mathf.Rad2Deg;
                moveDirection.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
            else
            {
                dirArrow.gameObject.SetActive(false);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("MonsterBullet"))
            {
                float damage = collision.gameObject.GetComponent<MonsterBullet>().Damage;
                Hit(damage);
                collision.gameObject.GetComponent<MonsterBullet>().SetFalse();
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Monster"))
            {
                float damage = other.gameObject.GetComponent<Monster>().AttackPower;
                Hit(damage);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Exp"))
            {
                other.GetComponent<EXPShard>().GetEXP(this);
            }
        }

        public void SetActiveSkill(ActiveSkill newActive)
        {
            if (AmountOfActive < actives.Length)
            {
                actives[AmountOfActive] = newActive;
                actives[AmountOfActive].ActiveSkillOn();
                AmountOfActive += 1;
                return;
            }
            else
                Debug.Log("Active Overflow!");
        }

        public void SetPassiveSkill(PassiveSkill newPassive)
        {
            if (AmountOfPassive < passives.Length)
            {
                passives[AmountOfPassive] = newPassive;
                AmountOfPassive += 1;
                return;
            }
            else
                Debug.Log("Passive Overflow!");
        }

        public void AddExp(int _exp)
        {
            exp += _exp;
            
            if (exp >= maxExp)
            {
                exp -= maxExp;
                maxExp *= 2;
                level += 1;
                chapterCtrl.LevelUp(ref actives, ref passives);
            }

            expBar.fillAmount = exp / maxExp;
        }

        public void Hit(float damage)
        {
            hp -= damage;
            hpBar.fillAmount = hp / maxHp;
        }

        public void UpgradeRegeneration(float plus)
        {
            regeneration += plus;
        }

        public void UpgradeDuration(float plus)
        {
            duration += plus;
        }

        public void UpgradeMaxHp(float plus)
        {
            maxHp += plus;
        }

        public void UpgradeAtk(float plus)
        {
            atk += plus;
        }

        public void UpgradeExpGain(float plus)
        {
            expGain += plus;
        }

        public void UpgradeMoveSpeed(float plus)
        {
            moveSpeed += plus;
        }
    }
}