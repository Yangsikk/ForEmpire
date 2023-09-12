using UnityEngine;
using Pathfinding;
using Cysharp.Threading.Tasks;

public class RangeUnit : AttackUnitModel {
    protected override void Awake() {
        base.Awake();
    }

    public override async void Attack() {
        base.Attack();
        while(isAttacking) {
            transform.LookAt(target);
            EventController.Event.Emit<SpawnProjectile>(new SpawnProjectile() {owner = this, type = ProjectileType.simple, target = target});
            await UniTask.Delay(750, cancellationToken:this.GetCancellationTokenOnDestroy());
        }
    }
}