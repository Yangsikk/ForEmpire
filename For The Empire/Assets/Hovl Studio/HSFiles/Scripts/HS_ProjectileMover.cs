using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;
using Cysharp.Threading.Tasks;
public class HS_ProjectileMover : MonoBehaviour
{
    public float speed = 15f;
    public float hitOffset = 0f;
    public bool UseFirePointRotation;
    public Vector3 rotationOffset = new Vector3(0, 0, 0);
    public GameObject hit;
    public GameObject flash;
    private Rigidbody rb;
    public GameObject[] Detached;
    public IObjectPool<BaseProjectile> pool;
    public RangeUnit owner;
    public RaycastHit raycast;
    CancellationTokenSource cts = new();
    public void Init(RangeUnit owner, IObjectPool<BaseProjectile> pool,float speed)
    {
        this.owner = owner;
        this.pool = pool;
        this.speed = speed;
        gameObject.layer =  owner.teamIndex == 0 ? 7 : 9;
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;
        if (flash != null)
        {
            //Instantiate flash effect on projectile position
            var flashInstance = Instantiate(flash, owner.transform.position + owner.transform.forward, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;
            
            //Destroy flash effect depending on particle Duration time
            var flashPs = flashInstance.GetComponent<ParticleSystem>();
            if (flashPs != null)
            {
                Destroy(flashInstance, flashPs.main.duration);
            }
            else
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPsParts.main.duration);
            }
        }
        Launch();
        Despawn().Forget();
	}
    public async UniTask Despawn() {
        await UniTask.Delay(3000, false, PlayerLoopTiming.Update, cancellationToken : cts.Token);
        pool.Release(transform.parent.gameObject.GetComponent<BaseProjectile>());
    }
    public void Launch() {
        transform.LookAt(owner.target.position + (Vector3.up * 2));
        rb.velocity = owner.transform.forward * speed;
    }
    void OnDisable() {
        cts.Cancel();
    }
    void OnCollisionEnter(Collision collision)
    {
        //Lock all axes movement and rotation
        rb.constraints = RigidbodyConstraints.FreezeAll;

        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point + contact.normal * hitOffset;

        //Spawn hit effect on collision
        if (hit != null)
        {
            var hitInstance = Instantiate(hit, pos, rot);
            if (UseFirePointRotation) { hitInstance.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180f, 0); }
            else if (rotationOffset != Vector3.zero) { hitInstance.transform.rotation = Quaternion.Euler(rotationOffset); }
            else { hitInstance.transform.LookAt(contact.point + contact.normal); }

            //Destroy hit effects depending on particle Duration time
            var hitPs = hitInstance.GetComponent<ParticleSystem>();
            if (hitPs != null)
            {
                Destroy(hitInstance, hitPs.main.duration);
            }
            else
            {
                var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitInstance, hitPsParts.main.duration);
            }
        }

        //Removing trail from the projectile on cillision enter or smooth removing. Detached elements must have "AutoDestroying script"
        foreach (var detachedPrefab in Detached)
        {
            if (detachedPrefab != null)
            {
                // detachedPrefab.transform.parent = null;
                // Destroy(detachedPrefab, 1);
            }
        }
        //Destroy projectile on collision
        pool.Release(transform.parent.gameObject.GetComponent<BaseProjectile>());
    }
}
