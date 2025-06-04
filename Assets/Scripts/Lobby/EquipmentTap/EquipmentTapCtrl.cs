using System;
using TMPro;
using UnityEngine;

namespace ZUN
{
    public class EquipmentTapCtrl : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI Hp;
        [SerializeField] TextMeshProUGUI Atk;
        [SerializeField] Transform[] inventoryTransform;
        public Transform[] InventoryTransform => inventoryTransform;
        [SerializeField] readonly EquipmentSlot[] equipedItemSlots = new EquipmentSlot[Enum.GetValues(typeof(EquipmentType)).Length];
        public EquipmentSlot[] EquipedItemSlots => equipedItemSlots;
        [SerializeField] Transform equipmentArea;
        public Transform EquipmentArea => equipmentArea;
        [SerializeField] Transform itemArea;
        public Transform ItemArea => itemArea;
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
            for (int i = 0; i < status.Inventory.Length; i++)
            {
                Equipment equipment = status.Inventory[i];
                if (equipment != null)
                {
                    EquipmentSlot slot = Instantiate(equipmentSlotPrefab, inventoryTransform[i]);
                    slot.isEquipped = true;
                    slot.SetPopup(popup);
                    slot.SetItem(equipment);
                    EquipedItemSlots[i] = slot;
                }
            }

            // Set Storage EquipmentSlot
            foreach (Equipment equipment in storage.equipments)
            {
                EquipmentSlot slot = Instantiate(equipmentSlotPrefab, equipmentArea);
                slot.isEquipped = false;
                slot.SetPopup(popup);
                slot.SetItem(equipment);
            }

            // Set Storage ItemSlot
            foreach (var itemDic in storage.items)
            {
                ItemSlot slot = Instantiate(itemSlotPrefab, itemArea);
                slot.SetTooltip(tooltip);
                slot.SetItem(itemDic.Value);
            }
        }
    }
}