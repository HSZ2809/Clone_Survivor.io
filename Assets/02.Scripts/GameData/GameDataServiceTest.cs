using System;
using UnityEngine;

namespace ZUN
{
    public static class GameDataServiceTest
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem("Tests/GameDataService/Run All")]
        public static void RunAllTests()
        {
            try
            {
                TestInMemoryRepository();
                TestGameDataService();

                Debug.Log("[GameDataServiceTest] All tests passed.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[GameDataServiceTest] Test failure: {ex.Message}\n{ex.StackTrace}");
            }
        }
#endif

        private class AggregateData
        {
            public string Id;
            public string Label;

            public AggregateData(string id, string label)
            {
                Id = id;
                Label = label;
            }
        }

        private class FakeEquipment : Equipment
        {
            public FakeEquipment(string uuid, EquipmentData data, EquipmentTier tier, int level)
                : base(uuid, data, tier, level)
            {
            }

            public override void SetTierEffect(Character character)
            {
                // no-op
            }
        }

        private class FakeEquipmentFactory : IEntityFactory<EquipmentInfo, Equipment>
        {
            private readonly Equipment _equipment;
            public bool ShouldFail { get; set; }

            public FakeEquipmentFactory(Equipment equipment)
            {
                _equipment = equipment;
            }

            public bool TryCreate(EquipmentInfo info, out Equipment entity)
            {
                entity = null;

                if (ShouldFail || info == null)
                    return false;

                entity = _equipment;
                return true;
            }
        }

        private class FakeItemFactory : IEntityFactory<ItemInfo, Item>
        {
            private readonly Item _item;
            public bool ShouldFail { get; set; }

            public FakeItemFactory(Item item)
            {
                _item = item;
            }

            public bool TryCreate(ItemInfo info, out Item entity)
            {
                entity = null;

                if (ShouldFail || info == null)
                    return false;

                entity = _item;
                return true;
            }
        }

        private static void Assert(bool condition, string message)
        {
            if (!condition)
                throw new InvalidOperationException(message);
        }

        private static void TestInMemoryRepository()
        {
            var values = new[]
            {
                new AggregateData("a", "One"),
                new AggregateData("b", "Two")
            };

            var repo = new InMemoryRepository<AggregateData, string>(values, x => x.Id);

            Assert(repo.Contains("a"), "Repository should contain key 'a'.");

            Assert(repo.TryGet("b", out var foundB), "TryGet for existing key should return true.");
            Assert(foundB.Label == "Two", "Retrieved value does not match expected label.");

            Assert(!repo.TryGet("c", out _), "TryGet for missing key should return false.");

            Assert(repo.GetAll().Count == 2, "GetAll should return all elements.");
        }

        private static void TestGameDataService()
        {
            var dummyData = new ItemData();
            var dummyItem = new Item(dummyData, 1);
            var fakeItemFactory = new FakeItemFactory(dummyItem);

            var fakeEquipment = new FakeEquipment("uuid", null, EquipmentTier.Common, 1);
            var fakeEquipFactory = new FakeEquipmentFactory(fakeEquipment);

            var service = new GameDataService(fakeEquipFactory, fakeItemFactory);

            Assert(service.TryCreateEquipment(new EquipmentInfo { Id = "any", Level = 1, Tier = EquipmentTier.Common }, out var createdEquipment), "Service should create equipment.");
            Assert(createdEquipment == fakeEquipment, "Created equipment instance mismatch.");

            Assert(service.TryCreateItem(new ItemInfo { Uid = "any", Amount = 1 }, out var createdItem), "Service should create item.");
            Assert(createdItem == dummyItem, "Created item instance mismatch.");

            fakeItemFactory.ShouldFail = true;
            Assert(!service.TryCreateItem(new ItemInfo { Uid = "any", Amount = 1 }, out _), "Service should fail when factory fails.");
        }
    }
}
