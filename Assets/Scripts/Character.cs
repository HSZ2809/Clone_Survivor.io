using UnityEngine;

namespace ZUN
{
    public class Character : MonoBehaviour
    {
        [Header("Connected Components")]
        [SerializeField] private Manager_Stage manager_Stage = null;
        [Space]
        [SerializeField] private Collider2D playerCollider = null;
        [SerializeField] private Transform shootDirection = null;
        [SerializeField] private Weapon[] weapons = new Weapon[6];
        [SerializeField] private Passive[] passives = new Passive[6];

        [Header("Connected Joystick")]
        [SerializeField] private Joystick joystick;

        [Header("Status")]
        [SerializeField] int level = 1;
        [SerializeField] private float hp = 0.0f;
        [SerializeField] private float moveSpeed = 0.0f;
        [SerializeField] private float attackPower = 0.0f;
        [SerializeField] private float attackSpeed = 0.0f;
        [SerializeField] private int exp = 0;
        [SerializeField] private string initialWeaponSN = "default";

        [SerializeField] private int amountOfWeapon = 0;
        [SerializeField] private int amountOfPassive = 0;

        public float AttackPower { get { return attackPower; } }
        public string InitialWeaponSN { get { return initialWeaponSN; } }
        public Transform GetShootDirection() { return shootDirection; }

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
                shootDirection.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Monster"))
            {
                float damage = other.gameObject.GetComponent<Monster>().AttackPower;
                hp -= damage;
                Debug.Log("Player : hit! - damage : " + damage);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Exp"))
            {
                AddExp(other.GetComponent<DropedEXP>().Exp);
                Destroy(other.gameObject);
            }
        }

        public void RegistrationWeapon(Weapon newWeapon)
        {
            if (amountOfWeapon < weapons.Length)
            {
                weapons[amountOfWeapon] = newWeapon;
                weapons[amountOfWeapon].ActivateWeapon(attackSpeed);
                amountOfWeapon += 1;
                return;
            }
            else
                Debug.Log("Weapon Overflow!");
        }

        public void RegistrationPassive(Passive newPassive)
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

            if (exp >= 10)
            {
                exp -= 10;
                level += 1;

                manager_Stage.ShowOptions(amountOfWeapon, GetWISNs(), amountOfPassive);
            }
        }

        private string[] GetWISNs()
        {
            string[] SNs = new string[6];

            for (int i = 0; i < 6; i++)
            {
                if (weapons[i] == null)
                    break;
                else
                {
                    SNs[i] = weapons[i].SerialNumber;
                }
            }

            return SNs;
        }
    }
}