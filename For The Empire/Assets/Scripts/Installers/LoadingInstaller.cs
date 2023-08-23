using UnityEngine;
using Zenject;

public class LoadingInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<LoadingProcess>().AsSingle().NonLazy();
    }

}