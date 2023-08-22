using UnityEngine;
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

        public void Awake() {
            init = Container.Resolve<InitProcess>();
            init.Initialize();
            loading = Container.Resolve<LoadingProcess>();
            loading.LoadScene("LobbyScene");
        }
        
    }
}