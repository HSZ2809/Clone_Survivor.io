using Unity.VisualScripting;
using UnityEngine;

namespace ZUN
{
    public class InfiniteScroll : MonoBehaviour
    {
        Camera mainCam;
        private float moveSpeed = 80.0f;
        private Vector2 direction;
        Vector3 camPos;
        Vector2 returnPoint;

        private void Awake()
        {
            mainCam = Camera.main;
        }

        private void Start()
        {
            float angle = 65.0f * Mathf.Deg2Rad;
            direction.x = Mathf.Cos(angle);
            direction.y = Mathf.Sin(angle);
            camPos = mainCam.transform.position;
            returnPoint = new Vector2(-1350f, -3000f);
        }

        private void FixedUpdate()
        {
            if (transform.position.y > 3000.0f)
            {
                transform.Translate(returnPoint);
            }
            else
                transform.Translate(moveSpeed * Time.deltaTime * direction);
        }
    }
}