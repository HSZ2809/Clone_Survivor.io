using UnityEngine;

namespace ZUN
{
    public class SupplyBox : MonoBehaviour
    {
        [SerializeField] GameObject supply;
        
        public Character character;
        [SerializeField] float maxDistance;

        private void FixedUpdate()
        {
            if (maxDistance < Vector3.Distance(character.transform.position, transform.position))
                Destroy(this);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Bullet"))
            {
                Instantiate(supply, transform.position, transform.rotation);
                Destroy(this);
            }
        }
    }
}
