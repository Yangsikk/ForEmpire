using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public abstract class BaseInstaller : MonoInstaller {
    protected LoadingProcess loading;
    public override void InstallBindings() {
        Container.Bind<LoadingProcess>().AsSingle();
    }
    protected virtual void Awake() {
        loading = Container.Resolve<LoadingProcess>();
    }
}