using UnityEngine;
using Pathfinding;

public class RangeUnit : AttackUnitModel {
    protected override void Awake() {
        base.Awake();
        destinationSetter = gameObject.AddComponent<AIDestinationSetter>();
    }
}