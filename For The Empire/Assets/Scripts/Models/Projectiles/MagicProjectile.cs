using UnityEngine;

public class MagicProjectile : BaseProjectile {
    public override void Initialize() { 
        defaultPath = "Projectiles/Projectile 8";
        speed = 15f;
    }
}