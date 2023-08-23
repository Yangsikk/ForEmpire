using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game {
    public class InitInstaller : MonoInstaller
    {
        InitProcess init;
        LoadingProcess loading;
        public override void InstallBindings()
        {
            Container.Bind<InitProcess>().AsSingle();
            Container.Bind<LoadingProcess>().AsSingle();
        }

        private void Awake() {
            init = Container.Resolve<InitProcess>();
            loading = Container.Resolve<LoadingProcess>();
            init.Initialize();
            loading.LoadScene("LobbyScene");
        }
        private void OnDestroy() {

        }        
    }
}