using UnityEngine;
public class SpawnUnit : IEvent {
    public GameObject gameObject;
    public UnitType type;
    public UnitTribe tribe;
}
public class SpawnEnemy : IEvent {
    public GameObject gameObject;
    public Transform target;
}
