using UnityEngine;
using Pathfinding;
using MonsterLove.StateMachine;
using Cysharp.Threading.Tasks;

public abstract class AttackUnitModel : BaseUnitModel, IAttack, IMove {
    public AttackAbility attack {get ;set;}
    public bool isDelay = false;
    public MoveAbility move {get; set;}
    public Transform target {get; set;} = null;
    public AIDestinationSetter destinationSetter {get; set;}
    IAstarAI ai;
    Pathfinder pathfinder;
    protected override void Awake()
    {
        base.Awake();
        fsm = new StateMachine<UnitState, StateDriverUnity>(this);
        fsm.ChangeState(UnitState.Idle);
        pathfinder = gameObject.AddComponent<Pathfinder>();
        pathfinder.slowdownDistance = 5f;
        pathfinder.IsReached = OnReached;
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
            if(target != null) Attack();
            else fsm.ChangeState(UnitState.Idle);
            pathfinder.OnTargetReached();
        }
    }
    public void SetDest(Transform target) {
        destinationSetter.target = target;
        fsm.ChangeState(UnitState.Move);
    }
    public void OnReached() {
        destinationSetter.target = null;
    }
    public void Attack() {
        fsm.ChangeState(UnitState.Attack);
        if(isDelay) return;
        if(target is not ILife life) return;
        if(!life.life.isAlive) return;
        if(life.life.Damage(25, gameObject)) {
            Delay();
        }
        else {
            Debug.Log("Kill Target");
            target.GetComponent<BaseUnitModel>().fsm.ChangeState(UnitState.Die);
            target = null;
        }
    }
    protected async void Delay() {
        isDelay = true;
        await UniTask.Delay(1000);
        isDelay = false;
    }
     private void OnTriggerEnter(Collider collider) {
        if(!collider.gameObject.CompareTag("Unit") && !collider.gameObject.CompareTag("Building")) return;
        if(!CheckTarget(collider.gameObject)) return;
        target = collider.transform;
        SetDest(collider.transform);
    }
    private void OnTriggerExit(Collider collider) {
        if(!collider.gameObject.CompareTag("Unit") && !collider.gameObject.CompareTag("Building")) return;
    }
    private void OnCollisionEnter(Collision collider) {
        if(!collider.gameObject.CompareTag("Unit") && !collider.gameObject.CompareTag("Building")) return;
    }
}
