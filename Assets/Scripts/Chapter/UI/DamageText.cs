using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

namespace ZUN
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] TextMeshPro tmp_damage;

        IObjectPool<DamageText> pool;
        readonly WaitForSeconds waitTime = new(1.0f);

        private void OnEnable()
        {
            StartCoroutine(Floating());
        }

        private void FixedUpdate()
        {
            transform.Translate(Vector3.up * Time.deltaTime);
        }

        IEnumerator Floating()
        {
            yield return waitTime;

            pool.Release(this);
        }

        public void SetText(float damage)
        {
            tmp_damage.text = damage.ToString("F0");
        }

        public void SetPool(IObjectPool<DamageText> pool)
        {
            this.pool = pool;
        }
    }
}