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
        CreatePools();
    }
    public void Release() {
        EventController.Event.Off<SpawnProjectile>(OnSpawnProejectile);
    }
    public void CreatePools() {
        CreatePool(ProjectileType.simple, CreateSimpleProjectile);
        CreatePool(ProjectileType.ice, CreateIceProjectile);
        CreatePool(ProjectileType.magic, CreateMagicProjectile);
    }
    private void CreatePool(ProjectileType type, System.Func<GameObject> action) {
        var pool = new ObjectPool<GameObject>(action, OnGet, OnRelease, OnDestroy, true, defaultSize, maxSize);
        for(var i = 0 ; i < defaultSize; i++) {
            var go = action();
            pool.Release(go);
        }
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
        go.SetActive(true);
    }
    private void OnRelease(GameObject go) {
        go.SetActive(false);
    }
    private void OnDestroy(GameObject go) {
        GameObject.Destroy(go);
    }
    private GameObject CreateProjectile<T>(string path) where T: BaseProjectile {
        var go = GameObject.Instantiate(Resources.Load<GameObject>(path));
        go.AddComponent<T>();
        return go;
    }

    public void OnSpawnProejectile(SpawnProjectile e)  {
        Debug.Log(ProjectilePool.ContainsKey(e.type));
        Debug.Log(ProjectilePool.TryGetValue(e.type, out var pool));
        var go = pool.Get();
        OnDespawn(pool, go);
    }
    public async void OnDespawn(IObjectPool<GameObject> pool, GameObject go) {
        await UniTask.Delay(5000);
        if(go == null) return;
        pool.Release(go);
    }
}