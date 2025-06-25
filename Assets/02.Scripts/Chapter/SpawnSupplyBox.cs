using UnityEngine;

namespace ZUN
{
    public class SpawnSupplyBox : MonoBehaviour
    {
        [SerializeField] GameObject supplyBox;
        [SerializeField] GameObject[] location;

        public void SetSupplyBox()
        {
            int randomLocation = Random.Range(0, location.Length);

            Instantiate(supplyBox).transform.position = location[randomLocation].transform.position;
        }
    }
}