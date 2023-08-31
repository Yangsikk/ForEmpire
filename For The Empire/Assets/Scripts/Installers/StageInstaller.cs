using UnityEngine;
using Zenject;

namespace Game {
    public class StageInstaller : BaseInstaller
    {
        SpawnProcess spawn;
        UnitProcess unit;
        Transform target;
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
        }
        private void OnDestroy() {
            GameObject.Destroy(this);
        }        
    }
}