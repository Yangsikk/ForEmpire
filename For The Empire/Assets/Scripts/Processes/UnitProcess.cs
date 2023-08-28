using UnityEngine;

public class UnitProcess {
    public void Initialize() {
        EventController.Event.On<SpawnUnit>(OnSpawnUnit);
    }
    public void Release() {
        EventController.Event.Off<SpawnUnit>(OnSpawnUnit);
    }

    public void OnSpawnUnit(SpawnUnit e) {
        switch(e.tribe) {
            case UnitTribe.Human : 
                if(e.type == UnitType.Melee) {
                    VHumanKnight knight = new();
                    var go = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(knight.prefabPath));
                    go.transform.SetParent(e.gameObject.transform);
                    go.AddComponent<MeleeUnit>();
                }
            break;
        }
    }
}