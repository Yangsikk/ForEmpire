using UnityEngine;
using Pathfinding;
using MonsterLove.StateMachine;
public class MeleeUnit : BaseUnitModel, IAttack, IMove {
    public AttackAbility attack {get ;set;}
    public MoveAbility move {get; set;}
    public ITarget target {get; set;}
    public AIDestinationSetter destinationSetter {get; set;}
    IAstarAI ai;
    AIPath path;
    protected override void Awake() {
        base.Awake();
        fsm = new StateMachine<UnitState, StateDriverUnity>(this);
        fsm.ChangeState(UnitState.Idle);
        path = gameObject.AddComponent<AIPath>();
        path.slowdownDistance = 5f;
        destinationSetter = gameObject.AddComponent<AIDestinationSetter>();
        var rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.isKinematic = true;
        var sphere = gameObject.AddComponent<SphereCollider>();
        sphere.isTrigger = true;
        sphere.radius = 15f;
        ai = GetComponent<IAstarAI>();
        ai.maxSpeed = 4f;
        ai.radius = 1f;
    }
    public void Update() {
        if(destinationSetter.target == null) return;
        if(Vector3.Distance(transform.position, destinationSetter.target.position) < 5) {
            destinationSetter.target = transform;
            path.OnTargetReached();
            fsm.ChangeState(UnitState.Idle);
        }
    }
    private void OnStop() {
        destinationSetter.target = null;
    }
    public void SetDest(Transform target) {
        destinationSetter.target = target;
        fsm.ChangeState(UnitState.Move);
    }
    private void OnTriggerEnter(Collider collider) {
        if(!collider.gameObject.CompareTag("Unit") && !collider.gameObject.CompareTag("Building")) return;
        SetDest(collider.transform);
        Debug.Log($"enter : {collider}");
    }
    
    private void OnTriggerExit(Collider collider) {
        if(!collider.gameObject.CompareTag("Unit") && !collider.gameObject.CompareTag("Building")) return;
        Debug.Log($"exit : {collider}");
    }
    private void OnCollisionEnter(Collision collider) {
        if(!collider.gameObject.CompareTag("Unit") && !collider.gameObject.CompareTag("Building")) return;
        Debug.Log(collider);
    }

}