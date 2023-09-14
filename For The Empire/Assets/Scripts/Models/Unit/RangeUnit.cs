using UnityEngine;
using UnityEngine.Pool;
using Pathfinding;
using Cysharp.Threading.Tasks;

public class RangeUnit: AttackUnitModel   {
    int defaultSize = 15;
    int maxSize = 30;
    private IObjectPool<BaseProjectile> pool;
    protected override void Awake() {
        base.Awake();
        
    }
    public BaseProjectile CreateProjectile<T>() where T : BaseProjectile{
        var go = new GameObject("Projectile");
        var projectile = go.AddComponent<T>();
        projectile.Initialize();
        var bullet = GameObject.Instantiate(Resources.Load<GameObject>(projectile.defaultPath), transform.position + transform.forward + (Vector3.up * 2), Quaternion.identity);
        bullet.transform.SetParent(go.transform);
        return projectile;
    }
    public void CreatePool<T>() where T: BaseProjectile {
        pool = new ObjectPool<BaseProjectile>(CreateProjectile<T>, OnGetProjectile, OnReleaseProjectile, OnDestroyProjectile, true, defaultSize, maxSize);
    }
    private void OnGetProjectile(BaseProjectile go) {
        var bullet = go.transform.GetChild(0);
        bullet.position = transform.position + transform.forward + (Vector3.up * 2);
        bullet.GetComponent<HS_ProjectileMover>().Init(this, pool, go.speed);
        go.gameObject?.SetActive(true);
    }
    private void OnReleaseProjectile(BaseProjectile go) {
        go.gameObject?.SetActive(false);
    }
    private void OnDestroyProjectile(BaseProjectile go) {
        
    }
    public override async void Attack() {
        base.Attack();
        while(isAttacking) {
            transform.LookAt(target);
            var bullet = pool.Get();
            bullet.Initialize();
            await UniTask.Delay(500, cancellationToken:this.GetCancellationTokenOnDestroy());
        }
    }
    public void DamageTarget(Transform tr) {
        var unit = tr.GetComponent<BaseUnitModel>();
        if(unit == null) return;
        if(!unit.life.Damage(attack.power, gameObject)) {
            Debug.Log("Kill Target");
            isAttack = false;
            unit.fsm.ChangeState(UnitState.Die);
            target = null;
            fsm.ChangeState(UnitState.Idle);
        }
    }
}