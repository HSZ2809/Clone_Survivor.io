using UnityEngine;

namespace ZUN
{
    public class MidBossSpawner : MonoBehaviour
    {
        Character character;

        [Header("Mid Boss")]
        [SerializeField] Monster midBoss;
        [SerializeField] float hp;
        [SerializeField] float attackPower;

        [Header("Range")]
        [SerializeField] float minDistance = 16f;
        [SerializeField] float maxDistance = 18f;

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
        }

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

            Monster mon = Instantiate(midBoss, randomVec2, character.transform.rotation);
            mon.SetMonsterSpec(hp, attackPower);
            mon.gameObject.SetActive(true);
        }
    }
}