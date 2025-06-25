using UnityEngine;

namespace ZUN
{
    public class Bullet_Shield_Wave : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] string animationName;
        [SerializeField] float startTiming;

        private void Start()
        {
            animator.Play(animationName, 0, startTiming);
        }

        void RandomRotate()
        {
            float randomZ = Random.Range(0.0f, 360.0f);

            transform.rotation = Quaternion.Euler(0.0f, 0.0f, randomZ);
        }
    }
}