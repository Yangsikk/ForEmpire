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
                    var go = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(ResourcesPath.Instance.human_knight));
                    go.tag = "Unit";
                    go.transform.SetParent(e.gameObject.transform);
                    var melee = go.AddComponent<MeleeUnit>();
                    melee.Initialize(new AttackUnitData(){life = 50, attackRange = 5f, moveSpeed = 4f, detectRange = 10f, minPower = 10f, maxPower = 15f});
                    melee.teamIndex = 0;
                }
                else if(e.type == UnitType.Range) {
                    var go = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(ResourcesPath.Instance.human_archer));
                    go.transform.position = new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f));
                    go.tag = "Unit";
                    go.layer = 6;
                    go.transform.SetParent(e.gameObject.transform);
                    var melee = go.AddComponent<RangeUnit>();
                    melee.CreatePool<IceProjectile>();
                    melee.Initialize(new AttackUnitData(){life = 40, attackRange = 50f, moveSpeed = 2f, detectRange = 50f, minPower = 7.5f, maxPower = 10f});
                    melee.teamIndex = 0;
                }
            break;
        }
    }
    public void OnSpawnEnemy(SpawnEnemy e) {
        var go = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(ResourcesPath.Instance.human_knight));
        go.tag = "Unit";
        go.transform.SetParent(e.gameObject.transform);
        go.transform.localPosition = Vector3.zero;
        var melee = go.AddComponent<MeleeUnit>();
        melee.Initialize(new AttackUnitData(){life = 50, attackRange = 5f, moveSpeed = 4f, detectRange = 10f, minPower = 10f, maxPower = 15f});
        go.layer = 8;
        melee.teamIndex = 1;
        melee.SetDest(e.target.position);
    }
}