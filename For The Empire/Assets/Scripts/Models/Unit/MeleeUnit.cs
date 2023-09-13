using UnityEngine;
using Cysharp.Threading.Tasks;
using Pathfinding;
using MonsterLove.StateMachine;
public class MeleeUnit : AttackUnitModel {
    
    protected override void Awake() {
        base.Awake();
    }
    public async override void Attack() {
        base.Attack();
        if(target is not ILife life) return;
        // while(life.Damage(attack.power, gameObject)) {
        //     await UniTask.Delay(1500);
        // }
        // Debug.Log("Kill Target");
        // unit.fsm.ChangeState(UnitState.Die);
        // target = null;
        // SetDest(destination);
    }
}