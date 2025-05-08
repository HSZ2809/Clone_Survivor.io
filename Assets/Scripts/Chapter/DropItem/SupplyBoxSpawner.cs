using System.Collections;
using UnityEngine;

namespace ZUN
{
    public class SupplyBoxSpawner : MonoBehaviour
    {
        [SerializeField] SupplyBox supplyBox;

        [SerializeField] Transform[] spawnTransforms;

        readonly WaitForSeconds waitTime = new (60.0f);
        int amount = 1;

        private void Start()
        {
            StartCoroutine(SupplySpawn());
        }

        IEnumerator SupplySpawn()
        {
            yield return waitTime;

            for(int i = 0; i < amount; i++)
            {
                SpawnSupplyBox();
            }

            // amount += 1;
        }

        public void SpawnSupplyBox()
        {
            int randomIndex = Random.Range(0, spawnTransforms.Length);
            
            SupplyBox box = Instantiate(supplyBox);
            box.transform.position = spawnTransforms[randomIndex].position;
            // box.SetActive(true);
        }
    }
}
