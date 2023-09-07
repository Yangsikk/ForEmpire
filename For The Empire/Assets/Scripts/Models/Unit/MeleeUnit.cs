using UnityEngine;
using Cysharp.Threading.Tasks;
using Pathfinding;
using MonsterLove.StateMachine;
public class MeleeUnit : AttackUnitModel {
    
    protected override void Awake() {
        base.Awake();
        life = new(100);
        
    }
}