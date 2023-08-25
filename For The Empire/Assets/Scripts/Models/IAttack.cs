public interface IAttack {
    public AttackAbility attack {get; set;}
    public ITarget target {get; set;}
}

public class AttackAbility {
    public float minPower;
    public float maxPower;
    public float attackRange;
    public float detectRange;
    public float attackSpeed;
}