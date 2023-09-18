using UnityEngine;
using Zenject;

namespace Game {
    public class StageInstaller : BaseInstaller
    {
        SpawnProcess spawn;
        UnitProcess unit;
        ObjectPoolProcess pool;
        Transform target;
        public override void InstallBindings()
        {
            base.InstallBindings();
            Container.Bind<SpawnProcess>().AsSingle();
            Container.Bind<UnitProcess>().AsSingle();
            Container.Bind<ObjectPoolProcess>().AsSingle();
        }

        protected override async void Awake() {
            base.Awake();
            spawn = Container.Resolve<SpawnProcess>();
            unit = Container.Resolve<UnitProcess>();
            pool = Container.Resolve<ObjectPoolProcess>();

            pool.Initialize();
            spawn.Initialize();
            unit.Initialize();
            var t = await spawn.SpawnBuilding<BaseBuildingModel>("target", ResourcesPath.Instance.castle, new Vector3(0, 0.1f, -30f));
            target = t.transform; 
        }

        public void Update() {
            if(Input.GetMouseButtonDown(0)) {
                spawn.Spawn("test", UnitType.Range, UnitTribe.Human);
            }
            if(Input.GetKeyDown(KeyCode.S)) {
                var enemy = spawn.Spawn("enemy", SpawnType.Enemy);
                enemy.transform.position = new Vector3(30f, 0f, 20f);
                EventController.Event.Emit<SpawnEnemy>(new SpawnEnemy() {gameObject = enemy, target = target});
            }
        }
        private void OnDestroy() {
            GameObject.Destroy(this);
        }        
    }
}