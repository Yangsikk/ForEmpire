using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game {
    public class InitInstaller : BaseInstaller
    {
        InitProcess init;
        public override void InstallBindings()
        {
            base.InstallBindings();
            Container.Bind<InitProcess>().AsSingle();
        }

        protected override void Awake() {
            base.Awake();
            init = Container.Resolve<InitProcess>();
            init.Initialize();
            loading.LoadScene("LobbyScene");
        }
        private void OnDestroy() {
            GameObject.Destroy(this);
        }        
    }
}