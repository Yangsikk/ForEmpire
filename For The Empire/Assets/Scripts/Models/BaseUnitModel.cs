using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class BaseUnitModel : MonoBehaviour, ILife, ITarget{
    public LifeAbility life {get; set;}
    public bool isTargetable {get; set;} = true;
}