using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZUN
{
    public class GameEntityFactory : MonoBehaviour, IGameEntityFactory
    {
        [SerializeField] private GameDataAssets assets;

        public GameEntityComposer Composer { get; private set; }

        private void Awake()
        {
            assets = FindFirstObjectByType<GameDataAssets>();

            if (assets == null)
                throw new InvalidOperationException("GameDataAssets is not assigned.");

            Composer = new GameEntityComposer(assets);
        }

        public bool TryCreateEquipment(EquipmentInfo info, out Equipment equipment)
        {
            equipment = null;
            if (Composer == null)
                return false;

            return Composer.TryCreateEquipment(info, out equipment);
        }

        public Equipment CreateEquipment(EquipmentInfo info)
        {
            if (TryCreateEquipment(info, out var equipment))
            {
                return equipment;
            }

            throw new KeyNotFoundException($"Equipment not found for Id={info?.Id}");
        }

        public bool TryCreateItem(ItemInfo info, out Item item)
        {
            item = null;
            if (Composer == null)
                return false;

            return Composer.TryCreateItem(info, out item);
        }

        public Item CreateItem(ItemInfo info)
        {
            if (TryCreateItem(info, out var item))
            {
                return item;
            }

            throw new KeyNotFoundException($"Item not found for Uid={info?.Uid}");
        }
    }
}