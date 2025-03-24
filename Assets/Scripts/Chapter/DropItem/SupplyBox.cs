using UnityEngine;

namespace ZUN
{
    public class SupplyBox : MonoBehaviour
    {
        [SerializeField] IGetDropedItem supply;
        
        Character character;
        float maxDistance;

        private void FixedUpdate()
        {
            if (maxDistance < GetDistance(character.transform.position, transform.position))
                Destroy(this);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Bullet"))
            {
                Instantiate(supply);
                Destroy(this);
            }
        }
    }
}
