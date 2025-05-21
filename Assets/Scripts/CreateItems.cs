using UnityEngine;

namespace ZUN
{
    public class CreateItems : MonoBehaviour
    {
        public Manager_Inventory inventory;
        public ItemSlot testSlot;
        public EquipmentData[] equipmentDatas;

        private void Awake()
        {
            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_Inventory>(out inventory))
            {
                Debug.LogWarning("Inventory not found");
            }
        }

        public void CreateItem()
        {
            // inventory 에 equipmentDatas의 안 아이템 생성
            // 아이템 내용물 로그 띄우기
            // 그걸 버튼에 연결

            inventory.weapon = equipmentDatas[0].Create(EquipmentTier.Common) as Weapon;
            testSlot.SetItem(inventory.weapon);
        }
    }
}