using UnityEngine;

namespace ZUN
{
    public class EquipmentTapCtrl : MonoBehaviour
    {
        [SerializeField] ItemSlot itemSlotPrefab;

        Manager_Storage storage;
        Manager_Inventory inventory;

        private void Awake()
        {
            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_Inventory>(out inventory))
                Debug.LogWarning("Inventory not found");

            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_Storage>(out storage))
                Debug.LogWarning("Storage not found");
        }
    }
}