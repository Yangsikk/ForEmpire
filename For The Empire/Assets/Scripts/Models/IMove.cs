using UnityEngine;

public interface IMove {
    public MoveAbility move {get; set;}
}

public class MoveAbility {
    public float maxSpeed;
    public float speed;
    public Vector3 dest;
}