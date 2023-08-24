using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public abstract class BaseUnitModel : ILife, IMove, ITarget{
    public LifeAbility life {get; set;}
    public MoveAbility move {get; set;}
    public bool isTargetable {get; set;} = true;
}