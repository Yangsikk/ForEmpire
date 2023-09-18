using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public enum SpawnType {
    Unit,
    Building,
    Enemy
}
public enum UnitType {
    Melee,
    Range
}

public enum UnitTribe {
    Human,
    Dwarf,
    Dragon,
    Ghoul,
    Skull,
    Troll,
    Zombie
}

public class SpawnProcess {
    GameObject root;
    List<GameObject> gameObjects = new();
    public void Initialize() {
        root = new GameObject("GameObjects");
    }

    public void Clear() {
        gameObjects.RemoveRange(0, gameObjects.Count);
        GameObject.Destroy(root);
    }
    public GameObject Spawn(string name) {
        var go = new GameObject(name);
        go.transform.SetParent(root.transform);
        gameObjects.Add(go);
        return go;
    }
    public GameObject Spawn(string name, SpawnType type) {
        GameObject go;
        go = SpawnEnemy(name);
        switch(type) {
            case SpawnType.Enemy : 
            break;
        }

        return go;
    }
    public async UniTask<GameObject> SpawnBuilding<T>(string name, string path) where T : BaseBuildingModel {
        var go = await SpawnAsync(name, path);
        go.AddComponent<T>();
        go.tag = "Building";
        return go;
    }
    public async UniTask<GameObject> SpawnBuilding<T>(string name, string path, Vector3 pos) where T : BaseBuildingModel {
        var go = await SpawnBuilding<T>(name, path);
        go.transform.position = pos;
        return go;
    }
    public GameObject SpawnUnit<T>(string name) where T : BaseUnitModel {
        var go = Spawn(name);
        go.AddComponent<T>();
        go.tag = "Unit";
        return go;
    }
    public GameObject SpawnUnit<T>(string name, Vector3 pos) where T : BaseUnitModel {
        var go = SpawnUnit<T>(name);
        go.transform.position = pos;
        return go;
    }
    public async UniTask<GameObject> SpawnAsync(string name, string path) {
        var go = Spawn(name);
        var g = await Resources.LoadAsync<GameObject>(path) as GameObject;
        GameObject.Instantiate<GameObject>(g).transform.SetParent(go.transform);
        return go;
    }
    
    private GameObject SpawnEnemy(string name) {
        var go = Spawn(name);
        return go;
    }
    private GameObject SpawnUnit(string name) {
        return null;
    }
    public GameObject Spawn(string name, UnitType type, UnitTribe tribe) {
        var go = Spawn(name);
        EventController.Event.Emit<SpawnUnit>(new SpawnUnit() {gameObject = go, type = type, tribe = tribe});
        return go;
    }
    
}