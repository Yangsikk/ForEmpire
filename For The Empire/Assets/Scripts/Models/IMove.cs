using UnityEngine;
using Pathfinding;

public interface IMove {
    public MoveAbility move {get; set;}
    public AIDestinationSetter destinationSetter {get; set;}
}

public class MoveAbility {
    public float maxSpeed;
    public float speed;
    public Vector3 dest;
}