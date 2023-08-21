using UnityEngine;
using Zenject;

public class LoadingInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<string>().FromInstance("Hello");
        Container.Bind<Greetings>().AsSingle().NonLazy();
    }

}