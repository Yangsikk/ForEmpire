using UnityEngine;
using MonsterLove.StateMachine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public abstract class BaseUnitModel : MonoBehaviour, ILife, ITarget, IAnimator{
    public enum UnitState {
        Idle, Attack, Move, Die
    }
    public int teamIndex;
    public bool isAttack = false;
    public LifeAbility life {get; set;}
    public bool isTargetable {get; set;} = true;
    public StateMachine<UnitState, StateDriverUnity> fsm;
    public Animator animator {get; set;}
    protected Rigidbody rg;
    protected SphereCollider detectCollider;
    protected virtual void Awake() {
        animator = GetComponent<Animator>();
        rg = gameObject.AddComponent<Rigidbody>();
        detectCollider = gameObject.AddComponent<SphereCollider>();
        fsm.ChangeState(UnitState.Idle);
    }
    void Update()
	{
		fsm.Driver.Update.Invoke();
	}
    protected void Idle_Enter() {
        animator.SetBool("Move", false);
        animator.Play("Idle");
    }
    protected async void Attack_Enter() {
        isAttack = true;
        animator.SetBool("Move", false);
        while(isAttack) {
            animator.SetInteger("AttackIndex", 0);
            animator.SetTrigger("Attack");
            await UniTask.Delay(1000);
        }
    }
    protected void Attack_Exit() {
        isAttack = false;
    }
    protected void Move_Enter() {
        animator.SetBool("Move", true);
    }
    protected void Die_Enter() {
        Destroy(gameObject);
        var effect = GameObject.Instantiate(Resources.Load<GameObject>("Effects/GenericDeath"), transform.position + (Vector3.up * 5), Quaternion.identity);
        Destroy(effect, 2f);
    }
    protected bool CheckTarget(GameObject go) {
        var unitModel = go.GetComponent<BaseUnitModel>();
        if(unitModel == null) return false;
        if(unitModel.teamIndex == teamIndex) return false;
        return true;
    }
}