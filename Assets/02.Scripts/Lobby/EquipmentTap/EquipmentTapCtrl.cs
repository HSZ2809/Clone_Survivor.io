using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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
        [SerializeField] ScrollRect scrollRect;
        [SerializeField] EquipmentCtrl popup;
        [SerializeField] ItemTooltipCtrl tooltip;
        [SerializeField] CharacterDisplay characterDisplay;

        // [Header("Prefab")]
        // [SerializeField] EquipmentSlot equipmentSlotPrefab;
        // [SerializeField] ItemSlot itemSlotPrefab;
        [Inject] EquipmentSlot.Factory equipmentSlotFactory;
        [Inject] ItemSlot.Factory itemSlotFactory;

        public readonly List<EquipmentSlot> equipmentSlots = new();
        public readonly List<ItemSlot> itemSlots = new();

        [Inject] IManager_Storage storage;
        [Inject] IManager_Status status;

        private void Start()
        {
            // Set Atk, Hp
            Atk.text = status.FinalAtk.ToString();
            Hp.text = status.FinalHp.ToString();

            // Set Weapon Sprite
            if (status.Inventory[(int)EquipmentType.Weapon] != null)
                characterDisplay.SetWeapon(status.Inventory[(int)EquipmentType.Weapon].ItemSprite);
            else
                characterDisplay.SetWeapon(null);

            // Set Equipments Slot
            for (int i = 0; i < status.Inventory.Length; i++)
                {
                    Equipment equipment = status.Inventory[i];
                    if (equipment != null)
                    {
                        //EquipmentSlot slot = Instantiate(equipmentSlotPrefab, inventoryTransform[i]);
                        EquipmentSlot slot = equipmentSlotFactory.Create();
                        slot.transform.SetParent(inventoryTransform[i], false);
                        slot.transform.position = inventoryTransform[i].position;
                        slot.isEquipped = true;
                        slot.SetPopup(popup);
                        slot.SetEquipment(equipment);
                        EquipedItemSlots[i] = slot;
                    }
                }

            // Set Storage EquipmentSlot
            foreach (Equipment equipment in storage.Equipments)
            {
                //EquipmentSlot slot = Instantiate(equipmentSlotPrefab, equipmentArea);
                EquipmentSlot slot = equipmentSlotFactory.Create();
                slot.transform.SetParent(equipmentArea, false);
                slot.isEquipped = false;
                slot.SetPopup(popup);
                slot.SetEquipment(equipment);
                equipmentSlots.Add(slot);
            }

            // Set Storage ItemSlot
            foreach (var itemDic in storage.Items)
            {
                //ItemSlot slot = Instantiate(itemSlotPrefab, itemArea);
                ItemSlot slot = itemSlotFactory.Create();
                slot.transform.SetParent(itemArea, false);
                slot.SetTooltip(tooltip);
                slot.SetItem(itemDic.Value);
                itemSlots.Add(slot);
            }

            UpdateEquipSlots();
        }

        public void UpdateEquipSlots()
        {
            equipmentSlots.Sort(new EquipmentSlotComparer());
            for (int i = 0; i < equipmentSlots.Count; i++)
                equipmentSlots[i].transform.SetSiblingIndex(i);
        }

        public void UpdateItemSlots()
        {
            foreach (var itemSlot in itemSlots)
                itemSlot.UpdateSlot();
        }

        public void ResetScrollView()
        {
            scrollRect.verticalNormalizedPosition = 1.0f;
        }

        public void SetAtk()
        {
            // 시각효과 추가 필요
            Atk.text = status.FinalAtk.ToString();
        }

        public void SetHp()
        {
            // 시각효과 추가 필요
            Hp.text = status.FinalHp.ToString();
        }
    }
}