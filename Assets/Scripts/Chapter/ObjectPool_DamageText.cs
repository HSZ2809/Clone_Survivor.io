using UnityEngine;
using UnityEngine.Pool;

namespace ZUN
{
    public class ObjectPool_DamageText : MonoBehaviour
    {
        public IObjectPool<DamageText> Pool { get; private set; }
        [SerializeField] DamageText perfab;

        private void Awake()
        {
            Pool = new ObjectPool<DamageText>(CreateText, OnGetText, OnReleaseText, OnDestroyText, maxSize: 100);
        }

        DamageText CreateText()
        {
            DamageText damageText = Instantiate(perfab);
            damageText.SetPool(Pool);
            return damageText;
        }

        void OnGetText(DamageText damageText)
        {
            damageText.gameObject.SetActive(true);
        }

        void OnReleaseText(DamageText damageText)
        {
            damageText.gameObject.SetActive(false);
        }

        void OnDestroyText(DamageText damageText)
        {
            Destroy(damageText.gameObject);
        }
    }
}