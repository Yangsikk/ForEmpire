using UnityEngine;
using Pathfinding;

public class MeleeUnit : BaseUnitModel, IAttack, IMove {
    public AttackAbility attack {get ;set;}
    public MoveAbility move {get; set;}
    public ITarget target {get; set;}
    public AIDestinationSetter destinationSetter {get; set;}
    IAstarAI ai;
    public void Awake() {
        gameObject.AddComponent<AIPath>();
        destinationSetter = gameObject.AddComponent<AIDestinationSetter>();
        ai = GetComponent<IAstarAI>();
    }
    public void Update() {
        
    }
    public void SetDest(Transform target) {
        destinationSetter.target = target;
        ai.maxSpeed = 5;
    }
}