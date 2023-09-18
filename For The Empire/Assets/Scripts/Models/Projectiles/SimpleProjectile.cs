using UnityEngine;

public class SimpleProjectile : BaseProjectile {
    public override void Initialize() { 
        defaultPath = "Projectiles/Projectile 1";
        speed = 25f;
    }
}