using UnityEngine;

namespace ZUN
{
    public class SquareFenceCtrl : MonoBehaviour
    {
        public Fence fence;
        public Vector2Int gridSize;
        public Vector2 tileSize;
        public Vector2 startPosition;

        void Start()
        {
            SetFence();
        }

        void SetFence()
        {
            Vector2 position;

            // 상하단
            for (int x = 0; x < gridSize.x; x++)
            {
                position = new Vector2(startPosition.x + x * tileSize.x, startPosition.y + (gridSize.y - 1) * tileSize.y);
                InstantiateSpriteAtPosition(position);

                position = new Vector2(startPosition.x + x * tileSize.x, startPosition.y);
                InstantiateSpriteAtPosition(position);
            }

            // 좌우측
            for (int y = 1; y < gridSize.y - 1; y++)
            {
                position = new Vector2(startPosition.x, startPosition.y + y * tileSize.y);
                InstantiateSpriteAtPosition(position);

                position = new Vector2(startPosition.x + (gridSize.x - 1) * tileSize.x, startPosition.y + y * tileSize.y);
                InstantiateSpriteAtPosition(position);
            }
        }

        void InstantiateSpriteAtPosition(Vector2 position)
        {
            Fence instance = Instantiate(fence, position, Quaternion.identity);
            instance.transform.parent = this.transform;
        }
    }
}