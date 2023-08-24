using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using System.Collections.Generic;

public class LobbyUIProcess : UIProcess {
    Transform stageBtn;
    Transform dungeonBtn;

    public override void Initialize() {
        base.Initialize();
        stageBtn = canvas.Find("BottomPanel").Find("EnterStage");
        dungeonBtn = canvas.Find("BottomPanel").Find("EnterDungeon");

    }
    public void AddEnterStage(UnityAction action) {
        stageBtn.gameObject.AddComponent<EnterStage>().SetOnClick(action);
    }
    public void AddEnterDungeon(UnityAction action) {
        dungeonBtn.gameObject.AddComponent<EnterDungeon>().SetOnClick(action);
    }
}