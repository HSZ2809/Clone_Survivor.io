using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class Bullet_Guardian : Bullet
    {
        [SerializeField] private Transform sprite;

        [SerializeField] private float rotationSpeed = 1.0f;

        private void Update()
        {
            sprite.Rotate(0, 0, rotationSpeed *(Time.deltaTime));
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Monster"))
            {
                Monster monster = other.gameObject.GetComponent<Monster>();
                monster.Hit(damage);
            }
        }
    }
}