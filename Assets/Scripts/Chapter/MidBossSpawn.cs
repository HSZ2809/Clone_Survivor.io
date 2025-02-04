using UnityEngine;

namespace ZUN
{
    public class MidBossSpawn : MonoBehaviour
    {
        [Header("Mid Boss")]
        [SerializeField] Monster midBoss;
        [SerializeField] float hp;

        [Header("Range")]
        [SerializeField] float minDistance = 16f;
        [SerializeField] float maxDistance = 18f;

        public void SetMidBoss()
        {
            Vector2 randomVec2;
            float randomAngle;
            float randomDistance;
            float x;
            float y;

            randomAngle = Random.Range(0f, 2f * Mathf.PI);
            randomDistance = Random.Range(minDistance, maxDistance);

            x = Mathf.Cos(randomAngle) * randomDistance;
            y = Mathf.Sin(randomAngle) * randomDistance;

            randomVec2.x = transform.position.x + x;
            randomVec2.y = transform.position.y + y;

            Monster mon = Instantiate(midBoss, randomVec2, transform.rotation);
            mon.MaxHp = hp;
            mon.gameObject.SetActive(true);
        }
    }
}