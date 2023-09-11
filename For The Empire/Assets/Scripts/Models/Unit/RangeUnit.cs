using UnityEngine;
using Pathfinding;

public class RangeUnit : AttackUnitModel {
    protected override void Awake() {
        base.Awake();
    }

    public override void Attack() {
        base.Attack();
        EventController.Event.Emit<SpawnProjectile>(new SpawnProjectile() {owner = this, type = ProjectileType.ice, target = target});
    }
}