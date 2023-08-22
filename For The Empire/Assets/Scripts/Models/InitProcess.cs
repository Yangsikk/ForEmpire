using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game {
    public class InitProcess {
        public InitProcess() {
            
        }
        public AsyncOperation Initialize() {
            Debug.Log("Initialize");
            return SceneManager.LoadSceneAsync("LoadingScene", LoadSceneMode.Additive);
        }
    }
}