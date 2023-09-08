using UnityEngine;

public enum ProjectileType {
    simple, ice, magic
}
public abstract class BaseProjectile : MonoBehaviour,IProjectile {
    public bool IsLaunched {get; protected set;}
    public Vector3 startPostion {get => transform.position;}
    public Vector3 startDirection {get => (startPostion - targetPosition).normalized;}
    public Vector3 targetPosition {get; protected set;}
    public float speed { get; set;} = 5;
    public IAttack owner {get; protected set;}
    public virtual void Initialize() { 
    }
    public void Launch(Vector3 targetPos) {
        IsLaunched = true;
        targetPosition = targetPos;
        
    }
    public void Launch(Vector3 targetPos, float speed) {
        this.speed = speed;
        Launch(targetPos);
    }
}