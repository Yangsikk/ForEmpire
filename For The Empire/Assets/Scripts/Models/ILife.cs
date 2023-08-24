public interface ILife {
    public LifeAbility life {get; set;}
}

public class LifeAbility {
    public int maxHp;
    public int hp;
    public int shield;
    public float armor;
}