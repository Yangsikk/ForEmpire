using UnityEngine;
using UnityEngine.Pool;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public class ObjectPoolProcess {
    int defaultSize = 30;
    int maxSize = 100;
    public Dictionary<ProjectileType, IObjectPool<GameObject>> ProjectilePool = new();
    public void Initialize() {
        EventController.Event.On<SpawnProjectile>(OnSpawnProejectile);
        EventController.Event.On<DespawnPool>(OnDespawnPool);
        CreatePools();
    }
    public void Release() {
        EventController.Event.Off<SpawnProjectile>(OnSpawnProejectile);
        EventController.Event.Off<DespawnPool>(OnDespawnPool);
    }
    public void CreatePools() {
        CreatePool(ProjectileType.simple, CreateSimpleProjectile);
        CreatePool(ProjectileType.ice, CreateIceProjectile);
        CreatePool(ProjectileType.magic, CreateMagicProjectile);
    }
    private void CreatePool(ProjectileType type, System.Func<GameObject> func) {
        var pool = new ObjectPool<GameObject>(func, OnGet, OnRelease, OnDestroy, true, defaultSize, maxSize);
        ProjectilePool.Add(type, pool);
    }
    private GameObject CreateSimpleProjectile() {
        return CreateProjectile<SimpleProjectile>("Projectiles/Projectile 1");
    }
    private GameObject CreateIceProjectile() {
        return CreateProjectile<IceProjectile>("Projectiles/Projectile 5");
    }
    private GameObject CreateMagicProjectile() {
        return CreateProjectile<MagicProjectile>("Projectiles/Projectile 8");
    }
    
    private void OnGet(GameObject go) {
        go?.SetActive(true);
    }
    private void OnRelease(GameObject go) {
        go?.SetActive(false);
    }
    private void OnDestroy(GameObject go) {
        // GameObject.Destroy(go);
    }
    private GameObject CreateProjectile<T>(string path) where T: BaseProjectile {
        var go = GameObject.Instantiate(Resources.Load<GameObject>(path));
        go.AddComponent<T>();
        return go;
    }

    public async void OnSpawnProejectile(SpawnProjectile e)  {
        Debug.Log(e.owner.life.hp + " / " + e.owner.life.isAlive);
        ProjectilePool.TryGetValue(e.type, out var pool);
        var go = pool.Get();
        await UniTask.Delay(100);
        if(!e.owner.life.isAlive) return;
        go.transform.position = e.owner.transform.position + e.owner.transform.forward + (Vector3.up * 2);
        go.transform.LookAt(e.target.position + Vector3.up * 2);
        var projectile = go.GetComponent<BaseProjectile>();
        projectile.Initialize();
        go.GetComponent<HS_ProjectileMover>().Init(e.owner, pool, projectile.speed);

    }
    public void OnDespawnPool(DespawnPool e) {
        if(!e.go.activeSelf) return;
        e.pool.Release(e.go);
    }
}