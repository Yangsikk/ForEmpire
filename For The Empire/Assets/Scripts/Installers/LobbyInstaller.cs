using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LobbyInstaller : MonoInstaller
{
    LoadingProcess loading;
    public override void InstallBindings()
    {
        Container.Bind<LoadingProcess>().AsSingle();
    }
    public void Awake() {
        loading = Container.Resolve<LoadingProcess>();
        loading.LoadEnd();
    }
}