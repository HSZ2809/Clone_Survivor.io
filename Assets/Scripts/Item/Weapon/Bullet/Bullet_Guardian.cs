using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class Bullet_Guardian : Bullet
    {
        [SerializeField] private Transform sprite;

        [SerializeField] private float rotationSpeed = 1.0f;
        // [SerializeField] private float disableTime = 1.0f;
        // public float DisableTime { get; set; }


        //private void OnEnable()
        //{
        //    StartCoroutine(DisableBullet());
        //}

        private void Update()
        {
            sprite.Rotate(0, 0, rotationSpeed *(Time.deltaTime));
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.tag == "Monster")
            {
                Monster monster = other.gameObject.GetComponent<Monster>();
                monster.Hit(damage);
            }
        }

        //IEnumerator DisableBullet()
        //{
        //    yield return new WaitForSeconds(DisableTime);
        //    gameObject.SetActive(false);
        //}
    }
}