using UnityEngine;
using MonsterLove.StateMachine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public abstract class BaseUnitModel : MonoBehaviour, ILife, ITarget, IAnimator{
    public enum UnitState {
        Idle, Attack, Move, Die
    }
    public LifeAbility life {get; set;}
    public bool isTargetable {get; set;} = true;
    public StateMachine<UnitState, StateDriverUnity> fsm;
    public Animator animator {get; set;}
    protected virtual void Awake() {
        animator = GetComponent<Animator>();
    }
    void Update()
	{
		fsm.Driver.Update.Invoke();
	}
    protected void Idle_Enter() {
        Debug.Log("Enter Idle");
        animator.SetBool("Move", false);
        animator.Play("Idle");
    }
    protected void Attack_Enter() {
        animator.SetBool("Move", false);
        animator.SetInteger("AttackIndex", 0);
        animator.SetTrigger("Attack");
    }
    protected void Move_Enter() {
        animator.SetBool("Move", true);
    }
    protected void Die_Enter() {

    }
}