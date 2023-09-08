using UnityEngine;

public class MagicProjectile : BaseProjectile {
    public readonly string EffectPath = "Effects/Hit 8";
    public override void Initialize() { 
        speed = 15f;
    }
}