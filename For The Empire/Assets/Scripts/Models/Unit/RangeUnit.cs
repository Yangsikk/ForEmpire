using UnityEngine;
using Pathfinding;

public class RangeUnit : BaseUnitModel, IAttack, IMove {
    public AttackAbility attack {get ;set;}
    public MoveAbility move {get; set;}
    public ITarget target {get; set;}
    public AIDestinationSetter destinationSetter {get; set;}
    protected override void Awake() {
        base.Awake();
        destinationSetter = gameObject.AddComponent<AIDestinationSetter>();
    }
}