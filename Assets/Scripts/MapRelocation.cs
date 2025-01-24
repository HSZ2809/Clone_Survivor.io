using UnityEngine;

namespace ZUN
{
    public class MapRelocation : MonoBehaviour
    {
        [SerializeField] Transform tileTrans;
        [SerializeField] SpriteRenderer tileSpriteRen;
        private Camera mainCam;
        float tileSize;
        Vector3 pos;
        Vector3 camPos;

        private void Start()
        {
            mainCam = Camera.main;
            tileSize = tileSpriteRen.sprite.bounds.size.x * tileTrans.localScale.x;
        }

        private void FixedUpdate()
        {
            pos = transform.position;
            camPos = mainCam.transform.position;

            if (camPos.x > pos.x + tileSize)
            {
                pos.x += tileSize;
                transform.position = pos;
            }
            else if (camPos.x < pos.x - tileSize)
            {
                pos.x -= tileSize;
                transform.position = pos;
            }
            else if (camPos.y > pos.y + tileSize)
            {
                pos.y += tileSize;
                transform.position = pos;
            }
            else if (camPos.y < pos.y - tileSize)
            {
                pos.y -= tileSize;
                transform.position = pos;
            }
        }
    }
}