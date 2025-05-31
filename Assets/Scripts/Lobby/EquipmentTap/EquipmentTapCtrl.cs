using UnityEngine;
using TMPro;

namespace ZUN
{
    public class EquipmentTapCtrl : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI Hp;
        [SerializeField] TextMeshProUGUI Atk;
        [SerializeField] Transform[] equipmentTransform;
        [SerializeField] Transform content;
        [SerializeField] EquipmentCtrlPopup popup;
        [SerializeField] ItemTooltipCtrl tooltip;

        [SerializeField] EquipmentSlot equipmentSlotPrefab;
        [SerializeField] ItemSlot itemSlotPrefab;

        Manager_Storage storage;
        Manager_Status status;

        private void Awake()
        {
            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_Status>(out status))
                Debug.LogWarning("Inventory not found");

            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_Storage>(out storage))
                Debug.LogWarning("Storage not found");
        }

        private void Start()
        {
            // Set Hp, Atk
            Hp.text = status.FinalHp.ToString();
            Atk.text = status.FinalAtk.ToString();

            // Set Equipment Slot
            Weapon weapon = status.inventory.weapon;
            if (weapon != null)
            {
                EquipmentSlot slot = Instantiate(equipmentSlotPrefab, equipmentTransform[(int)EquipmentType.Weapom]);
                slot.SetPopup(popup);
                slot.SetItem(weapon);
            }

            Armor armor = status.inventory.armor;
            if (armor != null)
            {
                EquipmentSlot slot = Instantiate(equipmentSlotPrefab, equipmentTransform[(int)EquipmentType.Armor]);
                slot.SetPopup(popup);
                slot.SetItem(armor);
            }

            Necklace necklace = status.inventory.necklace;
            if (necklace != null)
            {
                EquipmentSlot slot = Instantiate(equipmentSlotPrefab, equipmentTransform[(int)EquipmentType.Necklace]);
                slot.SetPopup(popup);
                slot.SetItem(necklace);
            }

            Belt belt = status.inventory.belt;
            if (belt != null)
            {
                EquipmentSlot slot = Instantiate(equipmentSlotPrefab, equipmentTransform[(int)EquipmentType.Belt]);
                slot.SetPopup(popup);
                slot.SetItem(belt);
            }

            Gloves gloves = status.inventory.gloves;
            if (gloves != null)
            {
                EquipmentSlot slot = Instantiate(equipmentSlotPrefab, equipmentTransform[(int)EquipmentType.Gloves]);
                slot.SetPopup(popup);
                slot.SetItem(gloves);
            }

            Shoes shoes = status.inventory.shoes;
            if (shoes != null)
            {
                EquipmentSlot slot = Instantiate(equipmentSlotPrefab, equipmentTransform[(int)EquipmentType.Shoes]);
                slot.SetPopup(popup);
                slot.SetItem(shoes);
            }

            // Set Storage EquipmentSlot
            foreach (Equipment equipment in storage.equipments)
            {
                EquipmentSlot slot = Instantiate(equipmentSlotPrefab, content);
                slot.SetPopup(popup);
                slot.SetItem(equipment);
            }

            // Set Storage ItemSlot
            foreach (var itemDic in storage.items)
            {
                ItemSlot slot = Instantiate(itemSlotPrefab, content);
                slot.SetTooltip(tooltip);
                slot.SetItem(itemDic.Value);
            }
        }
    }
}