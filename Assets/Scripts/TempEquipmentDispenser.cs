using UnityEngine;

namespace ZUN
{
    public class TempEquipmentDispenser : MonoBehaviour
    {
        public Manager_Status status;
        public Manager_Storage storage;
        public GameStart gameStart;

        public WeaponData weaponData;
        public ArmorData armorData;
        public NecklaceData necklaceData;
        public BeltData beltData;
        public GlovesData glovesData;
        public ShoesData shoesData;

        private void Awake()
        {
            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_Status>(out status))
                Debug.LogWarning("Inventory not found");
            if (!GameObject.FindGameObjectWithTag("Manager").TryGetComponent<Manager_Storage>(out storage))
                Debug.LogWarning("Storage not found");
        }

        public void Start()
        {
            status.inventory.weapon = weaponData != null ? weaponData.Create(EquipmentTier.Common) as Weapon : null;
            status.inventory.armor = armorData != null ? armorData.Create(EquipmentTier.Common) as Armor : null;
            status.inventory.necklace = necklaceData != null ? necklaceData.Create(EquipmentTier.Common) as Necklace : null;
            status.inventory.belt = beltData != null ? beltData.Create(EquipmentTier.Common) as Belt : null;
            status.inventory.gloves = glovesData != null ? glovesData.Create(EquipmentTier.Common) as Gloves  : null;
            status.inventory.shoes = shoesData != null ? shoesData.Create(EquipmentTier.Common) as Shoes : null;

            if (weaponData != null)
            {
                for (int i = 0; i < 20; i++)
                {
                    storage.equipments.Add(weaponData.Create(EquipmentTier.Common));
                }
            }
                
            if (shoesData != null)
            {
                for (int i = 0; i < 20; i++)
                {
                    storage.equipments.Add(shoesData.Create(EquipmentTier.Common));
                }
            }

            gameStart.tempEquipmentDispenserLoding = true;
        }
    }
}