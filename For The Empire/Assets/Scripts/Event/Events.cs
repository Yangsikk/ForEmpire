using UnityEngine;
using UnityEngine.Pool;
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
    public Transform target;
}
public class DespawnPool : IEvent { 
    public IObjectPool<GameObject> pool;
    public GameObject go;
}