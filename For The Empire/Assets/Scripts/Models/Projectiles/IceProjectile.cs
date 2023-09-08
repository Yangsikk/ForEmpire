using UnityEngine;

public class IceProjectile : BaseProjectile {
    public readonly string  EffectPath = "Effects/Hit 5";
    public override void Initialize() { 
        speed = 30f;
    }
}