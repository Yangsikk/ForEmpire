using UnityEngine;
using UnityEngine.Pool;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public class ObjectPoolProcess {
    int defaultSize = 15;
    int maxSize = 30;
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

    public void OnSpawnProejectile(SpawnProjectile e)  {
        ProjectilePool.TryGetValue(e.type, out var pool);
        var go = pool.Get();
        go.transform.position = e.position;
        go.transform.LookAt(e.target);
        go.SetActive(true);
        var projectile = go.GetComponent<BaseProjectile>();
        projectile.Initialize();
        
        go.GetComponent<HS_ProjectileMover>().Init(pool, e.position, projectile.speed);
    }
    
    public void OnDespawnPool(DespawnPool e) {
        if(!e.go.activeSelf) return;
        e.pool.Release(e.go);
    }
}