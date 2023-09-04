using UnityEngine;

public class UnitProcess {
    public void Initialize() {
        EventController.Event.On<SpawnUnit>(OnSpawnUnit);
        EventController.Event.On<SpawnEnemy>(OnSpawnEnemy);
    }
    public void Release() {
        EventController.Event.Off<SpawnUnit>(OnSpawnUnit);
        EventController.Event.Off<SpawnEnemy>(OnSpawnEnemy);
    }

    public void OnSpawnUnit(SpawnUnit e) {
        switch(e.tribe) {
            case UnitTribe.Human : 
                if(e.type == UnitType.Melee) {
                    VHumanKnight knight = new();
                    var go = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(knight.prefabPath));
                    go.tag = "Unit";
                    go.transform.SetParent(e.gameObject.transform);
                    go.AddComponent<MeleeUnit>();
                }
            break;
        }
    }
    public void OnSpawnEnemy(SpawnEnemy e) {
        VHumanKnight knight = new();
        var go = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(knight.prefabPath));
        go.tag = "Unit";
        go.transform.SetParent(e.gameObject.transform);
        go.transform.localPosition = Vector3.zero;
        var melee = go.AddComponent<MeleeUnit>();
        melee.SetDest(e.target);
    }
}