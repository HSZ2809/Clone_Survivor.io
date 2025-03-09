using UnityEngine;

namespace ZUN
{
    public class OnFieldTimer : MonoBehaviour
    {
        [SerializeField] float disableTime;
        float timer;

        private void FixedUpdate()
        {
            timer += Time.deltaTime;

            if (timer >= disableTime)
                gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            timer = 0.0f;
        }
    }
}