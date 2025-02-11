using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class Bullet_Guardian : Bullet
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip clip;
        [SerializeField] private Transform sprite;

        [SerializeField] private float rotationSpeed = 1.0f;

        private void Update()
        {
            sprite.Rotate(0, 0, rotationSpeed *(Time.deltaTime));
        }

        private void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.gameObject.CompareTag("Monster"))
            {
                coll.gameObject.GetComponent<IMon_Damageable>().TakeDamage(damage);
                audioSource.PlayOneShot(clip);
            }
        }
    }
}