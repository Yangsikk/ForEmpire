using UnityEngine;
using Cysharp.Threading.Tasks;
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

        protected override void Awake() {
            base.Awake();
            spawn = Container.Resolve<SpawnProcess>();
            unit = Container.Resolve<UnitProcess>();
            pool = Container.Resolve<ObjectPoolProcess>();

            pool.Initialize();
            spawn.Initialize();
            unit.Initialize();
            
            SetBuildings();
            SpawnEnemies();
        }

        public void Update() {
            if(Input.GetMouseButtonDown(0)) {
                spawn.Spawn("test", UnitType.Range, UnitTribe.Human);
            }
        }
        async void SetBuildings() {
            var t = await spawn.SpawnBuilding<BaseBuildingModel>("target", ResourcesPath.Instance.castle, new Vector3(0, 0.1f, -30f));
            t.GetComponent<BaseBuildingModel>().Initialize(new BuildingData() {life = 500, armor = 30});
            var w = await spawn.SpawnBuilding<BaseBuildingModel>("wall", ResourcesPath.Instance.wall, new Vector3(0, 0.1f, -20f));
            w.GetComponent<BaseBuildingModel>().Initialize(new BuildingData() {life = 100, armor = 10});
            target = t.transform; 
        }
        async void SpawnEnemies() {
            await SpawnEnemy(5, 1000);
            await SpawnEnemy(3, 1000);
            await SpawnEnemy(1, 1000);
        }
        async UniTask SpawnEnemy(int count, int delay) {
            await UniTask.Delay(delay);
            for(var i = 0; i < count; i++) {
                var enemy = spawn.Spawn("enemy", SpawnType.Enemy);
                enemy.transform.position = new Vector3(Random.Range(-5f, 5f), 0f, 40f);
                EventController.Event.Emit<SpawnEnemy>(new SpawnEnemy() {gameObject = enemy, target = target});
            }

        }
        private void OnDestroy() {
            GameObject.Destroy(this);
        }        
    }
}