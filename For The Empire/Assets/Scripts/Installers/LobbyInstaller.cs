using UnityEngine;
using Zenject;

public class LobbyInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<LoadingProcess>().AsSingle();
    }
    public void Awake() {
        var loading = Container.Resolve<LoadingProcess>();
        loading.LoadEnd();
    }
}