using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using Zenject;

public class LoadingProcess {
    public void LoadScene(string name) {
        SceneManager.LoadScene(name);
        SceneManager.LoadScene("LoadingScene", LoadSceneMode.Additive);
        Debug.Log($"Load Scene : {name} ");
    }
    public async void LoadEnd() {
        await UniTask.Delay(3000);
        var operaion = SceneManager.UnloadSceneAsync("LoadingScene");
    }

}