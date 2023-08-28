using UnityEngine;
using Zenject;

namespace Game {
    public class StageInstaller : BaseInstaller
    {
        SpawnProcess spawn;
        UnitProcess unit;
        public override void InstallBindings()
        {
            base.InstallBindings();
            Container.Bind<SpawnProcess>().AsSingle();
            Container.Bind<UnitProcess>().AsSingle();
        }

        protected override void Awake() {
            base.Awake();
            spawn = Container.Resolve<SpawnProcess>();
            unit = Container.Resolve<UnitProcess>();

            spawn.Initialize();
            unit.Initialize();
        }

        public void Update() {
            if(Input.GetMouseButtonDown(0)) {
                spawn.Spawn("test", UnitType.Melee, UnitTribe.Human);
            }
        }
        private void OnDestroy() {
            GameObject.Destroy(this);
        }        
    }
}