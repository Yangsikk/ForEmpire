using UnityEngine;

public interface IProjectile {
    bool IsLaunched {get;}
    Vector3 startPostion {get;}
    Vector3 startDirection {get;}
    Vector3 targetPosition {get;}
    float speed { get; set;}
    IAttack owner {get;}
    void Launch(Vector3 targetPosition, float speed);
}