using UnityEngine;

namespace ZUN
{
    public class CameraBounds : MonoBehaviour
    {
        //private BoxCollider2D boxCollider;
        private Camera mainCamera;

        private void Awake()
        {
            //boxCollider = GetComponent<BoxCollider2D>();
            mainCamera = Camera.main;
            tag = "Wall";
            gameObject.layer = LayerMask.NameToLayer(tag);
        }

        private void Start()
        {
            UpdateBoxColliderSize();
        }

        private void UpdateBoxColliderSize()
        { 
            float screenHeight = mainCamera.orthographicSize * 2f;
            float screenWidth = screenHeight * mainCamera.aspect;

            //boxCollider.size = new Vector2(screenWidth, screenHeight);

            //boxCollider.offset = Vector2.zero;
        }
    }
}