using UnityEngine;

namespace ZUN
{
    public class DestroyFence : MonoBehaviour
    {
        private void RemoveFence()
        {
            GameObject[] fenceObjects = GameObject.FindGameObjectsWithTag("Fence");

            foreach (GameObject fenceObject in fenceObjects)
            {
                fenceObject.SendMessage("Die", SendMessageOptions.DontRequireReceiver);
            }
        }
    }
}