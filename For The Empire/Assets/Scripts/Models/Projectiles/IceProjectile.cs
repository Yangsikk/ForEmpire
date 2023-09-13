using UnityEngine;

public class IceProjectile : BaseProjectile {
    public override void Initialize() { 
        defaultPath = "Projectiles/Projectile 5";
        speed = 30f;
    }
}