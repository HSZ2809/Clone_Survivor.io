using UnityEngine;

namespace ZUN
{
    public class SupplyBox : MonoBehaviour
    {
        [SerializeField] GameObject supply;
        
        public Character character;
        [SerializeField] float maxDistance;

        private void Awake()
        {
            character = GameObject.FindGameObjectWithTag("Character").GetComponent<Character>();
        }

        private void FixedUpdate()
        {
            if (maxDistance < Vector3.Distance(character.transform.position, transform.position))
                Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Bullet"))
            {
                Instantiate(supply, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
