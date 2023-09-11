using UnityEngine;
using Cysharp.Threading.Tasks;
using Pathfinding;
using MonsterLove.StateMachine;
public class MeleeUnit : AttackUnitModel {
    
    protected override void Awake() {
        base.Awake();
        life = new(100);
        
    }
    public async override void Attack() {
        base.Attack();
        while(life.Damage(attack.power, gameObject)) {
            await UniTask.Delay(1000);
        }
        Debug.Log("Kill Target");
        target.GetComponent<BaseUnitModel>().fsm.ChangeState(UnitState.Die);
        target = null;
        SetDest(destination);
    }
}