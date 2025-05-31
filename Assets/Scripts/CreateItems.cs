using UnityEngine;

namespace ZUN
{
    public class CreateItems : MonoBehaviour
    {
        public Manager_Status status;
        public EquipmentSlot testSlot;
        public EquipmentData[] equipmentDatas;

        private void Awake()
        {
            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_Status>(out status))
            {
                Debug.LogWarning("Inventory not found");
            }
        }

        public void CreateItem()
        {
            // inventory 에 equipmentDatas의 안 아이템 생성
            // 아이템 내용물 로그 띄우기
            // 그걸 버튼에 연결

            status.inventory.weapon = equipmentDatas[0].Create(EquipmentTier.Common) as Weapon;
            testSlot.SetItem(status.inventory.weapon);
        }
    }
}