using Zenject;
using UnityEngine;

namespace ZUN
{
    // Attach to a SceneContext in Chapter_1 so the scene can run without ManagerScene.
    // When launched from ManagerScene normally, Manager_Status exists in a loaded scene
    // so this installer skips binding — real managers remain active.
    public class DummyInstaller : MonoInstaller
    {
        [Header("Dummy Status Values")]
        [SerializeField] float baseHp = 100f;
        [SerializeField] float baseAtk = 10f;

        public override void InstallBindings()
        {
            // SceneContext always calls ProjectContext.Instance.EnsureIsInitialized() before
            // InstallBindings, so ProjectContext.HasInstance is always true — unreliable as guard.
            // Instead, check for the concrete Manager_Status MonoBehaviour which only exists
            // when ManagerScene is loaded.
            if (Object.FindFirstObjectByType<Manager_Status>() != null) return;

            Container.Bind<IManager_Status>().FromInstance(new DummyManager_Status(baseHp, baseAtk));
            Container.Bind<IManager_Vibration>().FromInstance(new DummyManager_Vibration());
            Container.Bind<IManager_Audio>().FromInstance(new DummyManager_Audio());
            Container.Bind<IManager_Scene>().FromInstance(new DummyManager_Scene());
            Container.Bind<IManager_Alert>().FromInstance(new DummyManager_Alert());
            Container.Bind<IManager_JoystickSetting>().FromInstance(new DummyManager_JoystickSetting());
            Container.Bind<IManager_VisualEffect>().FromInstance(new DummyManager_VisualEffect());
            Container.Bind<IManager_Storage>().FromInstance(new DummyManager_Storage());
            Container.Bind<IGameEntityFactory>().FromInstance(new DummyGameEntityFactory());
        }
    }
}
