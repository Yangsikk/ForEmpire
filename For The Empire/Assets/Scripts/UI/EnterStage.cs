using UnityEngine;
using UnityEngine.UI;

public class EnterStage : MonoBehaviour {
    protected void Awake() {
        gameObject.GetComponent<Button>().onClick.AddListener(onClicked);
    }

    public void onClicked() {
        Debug.Log("Enter Stage");
    }
}