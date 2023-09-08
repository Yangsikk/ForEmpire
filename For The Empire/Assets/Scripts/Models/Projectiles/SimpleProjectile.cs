using UnityEngine;

public class SimpleProjectile : BaseProjectile {
    public readonly string EffectPath = "Effects/Hit 1";
    public override void Initialize() { 
        speed = 25f;
    }
}