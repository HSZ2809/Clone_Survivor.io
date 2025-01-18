using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace ZUN
{
    public class Character : MonoBehaviour
    {
        [Header("Connected Components")]
        [SerializeField] private Transform moveDirection = null;
        [SerializeField] private ActiveSkill[] actives = new ActiveSkill[6];
        [SerializeField] private PassiveSkill[] passives = new PassiveSkill[6];

        Manager_Stage manager_Stage;
        Manager_Inventory inventory;

        [Header("Connected Joystick")]
        [SerializeField] private Joystick joystick;

        [Header("Status")]
        [SerializeField] int level;
        [SerializeField] int exp;
        [SerializeField] float maxHp;
        [SerializeField] float currentHp;
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

        [Space]
        [SerializeField] private int amountOfActive = 0;
        [SerializeField] private int amountOfPassive = 0;

        public float MaxHp { get { return maxHp; } }
        public float Atk { get { return atk; } }
        public float AtkSpeed { get { return atkSpeed; } }
        public float BulletSpeed { get { return bulletSpeed; } }
        public float MoveSpeed { get { return moveSpeed; } }
        public float ExpGain { get { return expGain; } }
        public float Duration { get { return duration; } }
        public Transform GetShootDir() { return moveDirection; }

        private void Awake()
        {
            manager_Stage = GameObject.FindGameObjectWithTag("Manager_Stage").GetComponent<Manager_Stage>();
            inventory = GameObject.FindGameObjectWithTag("Manager_Inventory").GetComponent<Manager_Inventory>();
        }

        private void Start()
        {
            manager_Stage.AddSkillDic(inventory.Active);
            manager_Stage.InitSkill(inventory.Active.SkillName);
        }

        private void Update()
        {
            float h = joystick.GetHorizontalAxis();
            float v = joystick.GetVerticalAxis();

            Vector3 moveDirection = new Vector3(h, v, 0).normalized;
            moveDirection *= moveSpeed;

            transform.Translate(moveDirection * Time.deltaTime);

            if (h != 0.0f || v != 0.0f)
            {
                float angle = Mathf.Atan2(v, h) * Mathf.Rad2Deg;
                this.moveDirection.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Monster"))
            {
                float damage = other.gameObject.GetComponent<Monster>().AttackPower;
                currentHp -= damage;
                Debug.Log("Player : hit! - damage : " + damage);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Exp"))
            {
                AddExp(other.GetComponent<DropedEXP>().Exp);
                other.gameObject.SetActive(false);
            }
        }

        public void SetActiveSkill(ActiveSkill newActive)
        {
            if (amountOfActive < actives.Length)
            {
                actives[amountOfActive] = newActive;
                actives[amountOfActive].ActiveSkillOn();
                amountOfActive += 1;
                return;
            }
            else
                Debug.Log("Active Overflow!");
        }

        public void SetPassiveSkill(PassiveSkill newPassive)
        {
            if (amountOfPassive < passives.Length)
            {
                passives[amountOfPassive] = newPassive;
                amountOfPassive += 1;
                return;
            }
            else
                Debug.Log("Passive Overflow!");
        }

        public void AddExp(int _exp)
        {
            exp += _exp;

            if (exp >= 100)
            {
                exp = 0;
                level += 1;

                manager_Stage.LevelUp(ref actives, ref passives);
            }
        }

        public void SetSkill(string skillName)
        {
            int index = -1;

            index = Array.FindIndex(actives, element => element != null && element.ID == skillName);
            if(index > -1)
            {
                actives[index].Upgrade();
                return;
            }
                

            index = Array.FindIndex(passives, element => element != null && element.ID == skillName);
            if (index > -1)
            {
                passives[index].Upgrade();
                return;
            }

            manager_Stage.InitSkill(skillName);
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