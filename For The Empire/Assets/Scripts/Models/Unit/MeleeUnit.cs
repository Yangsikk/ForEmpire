using UnityEngine;
using Pathfinding;

public class MeleeUnit : BaseUnitModel, IAttack, IMove {
    public AttackAbility attack {get ;set;}
    public MoveAbility move {get; set;}
    public ITarget target {get; set;}
    public AIDestinationSetter destinationSetter {get; set;}
    public void Awake() {
        destinationSetter = gameObject.AddComponent<AIDestinationSetter>();
    }
}