using UnityEngine;

namespace ZUN
{
    public class InfiniteScroll : MonoBehaviour
    {
        Camera mainCam;
        private float moveSpeed = 80.0f;
        private Vector2 direction = new Vector2(0.8f, 1.0f);

        private void Awake()
        {
            mainCam = Camera.main;
        }

        private void FixedUpdate()
        {
            transform.Translate(moveSpeed * Time.deltaTime * direction);
        }
    }
}