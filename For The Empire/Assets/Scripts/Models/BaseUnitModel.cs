using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public abstract class BaseUnitModel : MonoBehaviour, ILife, ITarget{
    public LifeAbility life {get; set;}
    public bool isTargetable {get; set;} = true;
}