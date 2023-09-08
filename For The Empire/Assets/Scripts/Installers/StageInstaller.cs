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

        protected override void Awake() {
            base.Awake();
            spawn = Container.Resolve<SpawnProcess>();
            unit = Container.Resolve<UnitProcess>();
            pool = Container.Resolve<ObjectPoolProcess>();

            pool.Initialize();
            spawn.Initialize();
            unit.Initialize();
            target = spawn.Spawn("target", new Vector3(-50f, 0.1f, -10f)).transform;
        }

        public void Update() {
            if(Input.GetMouseButtonDown(0)) {
                spawn.Spawn("test", UnitType.Melee, UnitTribe.Human);
            }
            if(Input.GetKeyDown(KeyCode.S)) {
                var enemy = spawn.Spawn("enemy", SpawnType.Enemy);
                enemy.transform.position = new Vector3(30f, 0f, 20f);
                EventController.Event.Emit<SpawnEnemy>(new SpawnEnemy() {gameObject = enemy, target = target});
            }
            if(Input.GetKeyDown(KeyCode.Q)) {
                var x = Random.Range(-5f, 5f);
                var z = Random.Range(-5f, 5f);
                EventController.Event.Emit<SpawnProjectile>(new SpawnProjectile() {type = ProjectileType.simple, position = new Vector3(x, 1, z), target = target});
            }
            if(Input.GetKeyDown(KeyCode.W)) {
                var x = Random.Range(-5f, 5f);
                var z = Random.Range(-5f, 5f);
                EventController.Event.Emit<SpawnProjectile>(new SpawnProjectile() {type = ProjectileType.ice, position = new Vector3(x, 1, z), target = target});
            }
            if(Input.GetKeyDown(KeyCode.E)) {
                var x = Random.Range(-5f, 5f);
                var z = Random.Range(-5f, 5f);
                EventController.Event.Emit<SpawnProjectile>(new SpawnProjectile() {type = ProjectileType.magic, position = new Vector3(x, 1, z), target = target});
            }
        }
        private void OnDestroy() {
            GameObject.Destroy(this);
        }        
    }
}