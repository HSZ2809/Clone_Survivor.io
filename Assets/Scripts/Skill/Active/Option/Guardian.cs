using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class Guardian : ActiveSkill
    {
        [SerializeField] private Transform pivot = null;

        [Header("Spac")]
        [SerializeField] float coefficient;
        [SerializeField] float cooldown;
        [SerializeField] float rotationSpeed;
        [SerializeField] float range;
        [SerializeField] float duration;
        [SerializeField] int magazineSize;

        [Header("Bullet")]
        [SerializeField] private Bullet_Guardian prefab_bullet = null;
        [SerializeField] private List<Bullet_Guardian> objPool = null;

        IEnumerator enumerator;

        public float BulletDamage { get { return coefficient * character.Atk; } }

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
            enumerator = Shoot();
        }
        private void Update()
        {
            pivot.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        
        public override void ActiveSkillOn()
        {
            SetAngle();
        }

        IEnumerator Shoot()
        {
            while(true)
            {
                foreach (var obj in objPool)
                    obj.gameObject.SetActive(true);

                yield return new WaitForSeconds(duration);

                foreach (var obj in objPool)
                    obj.gameObject.SetActive(false);

                yield return new WaitForSeconds(cooldown * character.AtkSpeed);
            }
        }

        void SetAngle()
        {
            StopCoroutine(enumerator);

            foreach(var obj in objPool)
                obj.gameObject.SetActive(false);

            while(magazineSize > objPool.Count)
            {
                Bullet_Guardian bulletInstance = Instantiate(prefab_bullet, transform.position, transform.rotation);
                bulletInstance.transform.parent = pivot.transform;
                objPool.Add(bulletInstance);
            }

            float angle = (360f / magazineSize) * Mathf.Deg2Rad;

            for (int i = 0; i < objPool.Count; i++)
            {
                float x = Mathf.Cos(angle * i);
                float y = Mathf.Sin(angle * i);
                Vector3 temp = new(transform.position.x + (x * range), transform.position.y + (y * range), 0);

                objPool[i].gameObject.transform.position = temp;
                objPool[i].Damage = BulletDamage;
            }

            StartCoroutine(enumerator);
        }

        public override void Upgrade()
        {
            level += 1;

            switch (level)
            {
                case 2:
                    magazineSize += 1;
                    coefficient = 0.6f;
                    break;
                case 3:
                    magazineSize += 1;
                    coefficient = 0.7f;
                    break;
                case 4:
                    magazineSize += 1;
                    coefficient = 0.8f;
                    break;
                case 5:
                    magazineSize += 1;
                    coefficient = 0.9f;
                    break;
                case 6:
                    coefficient = 0.9f;
                    Debug.Log("Guardian TryUpgrade() : evolution");
                    break;
                default:
                    Debug.LogWarning("Guardian TryUpgrade() : invalid level");
                    break;
            }

            SetAngle();
        }
    }
}