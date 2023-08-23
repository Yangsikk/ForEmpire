using UnityEngine;
using UnityEngine.UI;

public class EnterDungeon : MonoBehaviour {
    protected void Awake() {
        gameObject.GetComponent<Button>().onClick.AddListener(onClicked);
    }

    public void onClicked() {
        
    }
}