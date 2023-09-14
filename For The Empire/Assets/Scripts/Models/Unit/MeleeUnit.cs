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
        var unit = target.GetComponent<BaseUnitModel>();
        while(unit.life.Damage(attack.power, gameObject)) {
            await UniTask.Delay(1500);
        }
        Debug.Log("Kill Target");
        unit.fsm.ChangeState(UnitState.Die);
        target = null;
        SetDest(destination);
    }
}