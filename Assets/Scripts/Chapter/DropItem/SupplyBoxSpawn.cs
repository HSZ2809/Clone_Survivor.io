using UnityEngine;

namespace ZUN
{
    public class SupplyBoxSpawn : MonoBehaviour
    {
        [SerializeField] SupplyBox supplyBox;

        [SerializeField] Transform[] spawnTransforms;

        public void SpawnSupplyBox()
        {
            int randomIndex = Random.Range(0, spawnTransforms.Length);
            
            SupplyBox box = Instantiate(supplyBox);
            box.transform.position = spawnTransforms[randomIndex].position;
            // box.SetActive(true);
        }
    }
}
