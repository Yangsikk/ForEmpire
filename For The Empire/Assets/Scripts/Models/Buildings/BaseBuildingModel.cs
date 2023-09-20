using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class BaseBuildingModel : MonoBehaviour, ILife, ITarget{
    
    public int teamIndex {get; set;} = 0;
    public LifeAbility life {get; set;}
    public bool isTargetable {get; set;} = true;
    protected Rigidbody rg;
    protected SphereCollider detectCollider;
    protected virtual void Awake() {
        rg = gameObject.AddComponent<Rigidbody>();
        rg.isKinematic = true;
    }
    public void Initialize(BuildingData data) {
        life = new(data.life);
        life.armor = data.armor;
    }
    protected bool CheckTarget(GameObject go) {
        var unitModel = go.GetComponent<BaseUnitModel>();
        if(unitModel == null) return false;
        if(unitModel.teamIndex == teamIndex) return false;
        return true;
    }
}