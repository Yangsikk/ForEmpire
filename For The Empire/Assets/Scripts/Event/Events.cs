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

public class SpawnProjectile : IEvent {
    public ProjectileType type;
    public Vector3 position;
    public Vector3 target;
}