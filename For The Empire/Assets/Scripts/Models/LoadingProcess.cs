using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LoadingProcess {
    public void LoadScene(string name) {
        SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Additive);
    }
    public void LoadEnd() {
        SceneManager.UnloadSceneAsync("LoadingScene");
    }

}