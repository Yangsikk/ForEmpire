using UnityEngine;
using System.Collections.Generic;

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
    public GameObject Spawn(string name, Vector3 pos) {
        var go = Spawn(name);
        go.transform.position = pos;
        return go;
    }
    public GameObject Spawn(string name, UnitType type, UnitTribe tribe) {
        var go = Spawn(name);
        EventController.Event.Emit<SpawnUnit>(new SpawnUnit() {gameObject = go, type = type, tribe = tribe});
        return go;
    }
    
}