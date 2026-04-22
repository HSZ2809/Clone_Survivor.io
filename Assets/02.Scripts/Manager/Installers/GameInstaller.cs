using Zenject;
using UnityEngine;

namespace ZUN
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] EquipmentSlot equipmentSlotPrefab;
        [SerializeField] ItemSlot itemSlotPrefab;

        public override void InstallBindings()
        {
            Container.Bind<IManager_FirebaseCore>().FromInstance(FindFirstObjectByType<Manager_FirebaseCore>()).AsSingle();
            Container.Bind<IManager_Alert>().FromInstance(FindFirstObjectByType<Manager_Alert>()).AsSingle();
            Container.Bind<IManager_Audio>().FromInstance(FindFirstObjectByType<Manager_Audio>()).AsSingle();
            Container.Bind<IManager_FirebaseAuth>().FromInstance(FindFirstObjectByType<Manager_FirebaseAuth>()).AsSingle();
            Container.Bind<IManager_FirebaseFirestore>().FromInstance(FindFirstObjectByType<Manager_FirebaseFirestore>()).AsSingle();
            Container.Bind<IManager_JoystickSetting>().FromInstance(FindFirstObjectByType<Manager_JoystickSetting>()).AsSingle();
            Container.Bind<IManager_Scene>().FromInstance(FindFirstObjectByType<Manager_Scene>()).AsSingle();
            Container.Bind<IManager_Status>().FromInstance(FindFirstObjectByType<Manager_Status>()).AsSingle();
            Container.Bind<IManager_Storage>().FromInstance(FindFirstObjectByType<Manager_Storage>()).AsSingle();
            Container.Bind<IManager_Vibration>().FromInstance(FindFirstObjectByType<Manager_Vibration>()).AsSingle();
            Container.Bind<IManager_VisualEffect>().FromInstance(FindFirstObjectByType<Manager_VisualEffect>()).AsSingle();

            Container.Bind<IGameEntityFactory>().FromInstance(FindFirstObjectByType<GameEntityFactory>()).AsSingle();
            Container.Bind<IUserDataManager>().FromInstance(FindFirstObjectByType<UserDataManager>()).AsSingle();

            Container.BindFactory<EquipmentSlot, EquipmentSlot.Factory>()
            .FromComponentInNewPrefab(equipmentSlotPrefab)
            .AsTransient();
            Container.BindFactory<ItemSlot, ItemSlot.Factory>()
            .FromComponentInNewPrefab(itemSlotPrefab)
            .AsTransient();
        }
    }
}