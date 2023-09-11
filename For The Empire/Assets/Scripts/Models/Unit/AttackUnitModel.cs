using UnityEngine;
using Pathfinding;
using MonsterLove.StateMachine;
using Cysharp.Threading.Tasks;

public abstract class AttackUnitModel : BaseUnitModel, IAttack, IMove {
    public AttackAbility attack {get ;set;}
    public bool isAttacking{get; set;} = false;
    public bool isDelay = false;
    public MoveAbility move {get; set;}
    public Transform target {get; set;} = null;
    public AIDestinationSetter destinationSetter {get; set;}
    public Vector3 destination {get; set;}
    Pathfinder pathfinder;
    protected Transform moveTarget;
    protected override void Awake()
    {
        fsm = new StateMachine<UnitState, StateDriverUnity>(this);
        base.Awake();
        pathfinder = gameObject.AddComponent<Pathfinder>();
        destinationSetter = gameObject.AddComponent<AIDestinationSetter>();
        pathfinder.slowdownDistance = 5f;
        pathfinder.IsReached = OnReached;
        var go = new GameObject();
        go.transform.position = transform.position;
        moveTarget = go.transform;
        destination = transform.position;
        destinationSetter.target = moveTarget;
    }
    public void Initialize(AttackUnitData data) {
        life = new(data.life);
        attack = new AttackAbility() {detectRange = data.detectRange, attackRange = data.attackRange, minPower = data.minPower, maxPower = data.maxPower};
        rg.isKinematic = true;
        detectCollider.isTrigger = true;
        detectCollider.radius = data.detectRange;
        pathfinder.maxSpeed = data.moveSpeed;
        pathfinder.radius = 1f;
    }
    public void Update() {
        if(target == null) return;
        if(Vector3.Distance(transform.position, target.position) < attack.attackRange) {
            if(!isAttacking) {
                isAttacking = true;
                Attack();
            }
            pathfinder.OnTargetReached();
        }
    }
    public void SetDest(Vector3 destination) {
        this.destination = destination;
        moveTarget.position = destination;
        fsm.ChangeState(UnitState.Move);
    }
    public void SetTarget(Transform target) {
        fsm.ChangeState(UnitState.Move);
        this.target = target;
        destinationSetter.target = target;
    }
    public void OnReached() {
        moveTarget.position = transform.position;
        destinationSetter.target = moveTarget;
        if(!isAttacking) fsm.ChangeState(UnitState.Idle);
        else fsm.ChangeState(UnitState.Attack);
    }
    public virtual void Attack() {
        if(target is not ILife life) return;
        if(!life.life.isAlive) return;
        fsm.ChangeState(UnitState.Attack);
    }
    protected async UniTask Delay() {
        isDelay = true;
        await UniTask.Delay(1000);
        isDelay = false;
    }
     private void OnTriggerEnter(Collider collider) {
        if(!collider.gameObject.CompareTag("Unit") && !collider.gameObject.CompareTag("Building")) return;
        if(!CheckTarget(collider.gameObject)) return;
        if(Vector3.Distance(transform.position, collider.transform.position) > attack.detectRange + 1) return;
        SetTarget(collider.transform);
    }
    private void OnTriggerExit(Collider collider) {
        if(!collider.gameObject.CompareTag("Unit") && !collider.gameObject.CompareTag("Building")) return;
    }
    private void OnCollisionEnter(Collision collider) {
        if(!collider.gameObject.CompareTag("Unit") && !collider.gameObject.CompareTag("Building")) return;
    }
}
