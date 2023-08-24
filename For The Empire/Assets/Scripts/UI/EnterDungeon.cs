using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnterDungeon : Button {
    public void SetOnClick(UnityAction action) {
        onClick.AddListener(action);
    }
}