using Coffee.UIExtensions;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZUN
{
    public class Character : MonoBehaviour
    {
        #region Inspector
        [Header("Components")]
        [SerializeField] Transform moveDirection = null;
        [SerializeField] SpriteRenderer dirArrow;
        [SerializeField] Image expBar;
        [SerializeField] TextMeshProUGUI txt_level;
        [SerializeField] Image hpBar;
        [SerializeField] Image reloadBar;
        [SerializeField] UIParticle expMaxEffect;
        [SerializeField] ShowResult showResult;

        [Header("Skill")]
        [SerializeField] ActiveSkill[] actives = new ActiveSkill[6];
        [SerializeField] PassiveSkill[] passives = new PassiveSkill[6];

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
        #endregion

        ChapterCtrl chapterCtrl;
        Manager_Status status;
        Manager_Vibration vibration;
        ParticleSystem bleeding;

        bool isInLevelUpRoutine = false;
        readonly WaitForSeconds waitOneSec = new(1.0f);
        readonly WaitForSeconds waitFiveSec = new(5.0f);

        public int AmountOfActive { get; private set; }
        public int AmountOfPassive { get; private set; }

        public int Level => level;
        public float MaxExp => maxExp;
        public float Exp => exp;
        public float MaxHp => maxHp;
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
        public float Duration { get { return duration; } }
        public Transform GetMoveDir() { return moveDirection; }
        public Image ReloadBar() { return reloadBar; }

        public ActiveSkill[] Actives { get { return actives; } }
        public PassiveSkill[] Passives { get { return passives; } }

        private void Awake()
        {
            chapterCtrl = GameObject.FindGameObjectWithTag("ChapterCtrl").GetComponent<ChapterCtrl>();
            status = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager_Status>();
            vibration = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager_Vibration>();
            bleeding = GetComponent<ParticleSystem>();
        }

        private void Start()
        {
            maxHp = status.FinalHp;
            hp = maxHp;
            atk = status.FinalAtk;
        }

        private void Update()
        {
            float h = joystick.GetHorizontalAxis();
            float v = joystick.GetVerticalAxis();

            Vector3 direction = new Vector3(h, v, 0).normalized;

            transform.Translate(Time.deltaTime * moveSpeed * direction);

            if (h != 0.0f || v != 0.0f)
            {
                float angle = Mathf.Atan2(v, h) * Mathf.Rad2Deg;
                moveDirection.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("DropedItem"))
            {
                if (other.gameObject.TryGetComponent<IGetDropedItem>(out var dropedItem))
                {
                    dropedItem.GetDropedItem(this);
                }
            }
        }

        IEnumerator RegenerationHp()
        {
            while (true)
            {
                Hp += MaxHp * regeneration;
                hpBar.fillAmount = hp / maxHp;

                yield return waitFiveSec;
            }    
        }

        void LevelUp()
        {
            exp -= maxExp;
            maxExp *= 1.5f;
            level += 1;
            txt_level.text = "Lv." + level.ToString();
            expMaxEffect.gameObject.SetActive(true);
            expMaxEffect.Play();

            chapterCtrl.ShowLevelUpReward(ref actives, ref passives);
        }

        void Die()
        {
            showResult.gameObject.SetActive(true);
        }

        IEnumerator DelayLevelUpExecution()
        {
            yield return waitOneSec;

            LevelUp();
        }

        public void SetActiveSkill(ActiveSkill newActive)
        {
            if (AmountOfActive < actives.Length)
            {
                actives[AmountOfActive] = newActive;
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
            exp += _exp * expGain;

            if (exp >= maxExp && !isInLevelUpRoutine)
            {
                isInLevelUpRoutine = true;
                expBar.fillAmount = 1.0f;
                LevelUp();
            }
            else if (!isInLevelUpRoutine)
                expBar.fillAmount = exp / maxExp;
        }

        public void LevelUpCheck()
        {
            if (exp >= maxExp)
            {
                StartCoroutine(DelayLevelUpExecution());
            }
            else
            {
                expMaxEffect.Stop();
                expMaxEffect.gameObject.SetActive(false);
                expBar.fillAmount = exp / maxExp;
                isInLevelUpRoutine = false;
            }
        }

        public void GetTreasureBox()
        {
            chapterCtrl.StartLottery(ref actives, ref passives);
        }

        public void TakeDamage(float damage)
        {
            bleeding.Play();
            hp -= damage;
            hpBar.fillAmount = hp / maxHp;
            vibration.TriggerVibration();

            if (hp <= 0.0f)
                Die();
        }

        public void StartRegenerationHp()
        {
            StartCoroutine(RegenerationHp());
        }

        #region StatusUpgrade
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
        #endregion
    }
}