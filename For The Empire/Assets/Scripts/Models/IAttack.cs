using UnityEngine;
public interface IAttack {
    public AttackAbility attack {get; set;}
    public Transform target {get; set;}
    public void Attack();
}

public class AttackAbility {
    public float minPower;
    public float maxPower;
    public float attackRange;
    public float detectRange;
    public float attackSpeed;
}