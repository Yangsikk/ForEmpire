using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LobbyInstaller : BaseInstaller
{
    LobbyUIProcess uiProcess;
    public override void InstallBindings()
    {
        base.InstallBindings();
        Container.Bind<LobbyUIProcess>().AsSingle();
    }
    protected override void Awake() {
        base.Awake();
        loading.LoadEnd();
        uiProcess = Container.Resolve<LobbyUIProcess>();
        uiProcess.Initialize();
        uiProcess.AddEnterDungeon(() => { loading.LoadScene("DungeonScene"); });
        uiProcess.AddEnterStage(() => { loading.LoadScene("StageScene"); });
    }
}