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
        while(AttackTarget.Damage(attack.power, gameObject)) {
            await UniTask.Delay(1500);
        }
        Debug.Log("Kill Target");
        var unit = target.GetComponent<BaseUnitModel>();
        unit?.fsm.ChangeState(UnitState.Die);
        target = null;
        SetDest(destination);
    }
}