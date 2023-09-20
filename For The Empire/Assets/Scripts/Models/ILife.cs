using UnityEngine;
public interface ILife {
    public LifeAbility life {get; set;}
}

public class LifeAbility {
    public int maxHp;
    public int hp;
    public int shield;
    public float armor;
    public bool isAlive {get => hp > 0;}
    public GameObject lastAttacker;

    public LifeAbility(int hp) { maxHp = hp; this.hp = maxHp;}
    public LifeAbility(int currentHp, int maxHp) { hp = currentHp; this.maxHp = maxHp;}
    public bool Damage(float damage, GameObject attacker) {
        if(!isAlive) return false;
        lastAttacker = attacker;
        damage = damage - armor < 0 ? 1 : damage - armor;
        if(shield > 0) {
            damage = (int)damage - shield > 0 ? damage : 0;
        }
        hp -= (int)damage;
        hp = hp < 0 ? 0 : hp;
        Debug.Log($"Damaged {damage} by {attacker} - hp : {hp}");
        return isAlive;
    }
    public void Heal(int amount) {
        if(!isAlive) return;
        hp = hp + amount > maxHp ? maxHp : hp + amount;
    }
    public void Increase(int amount) {
        if(!isAlive) return;
        hp += amount;
        maxHp += amount;
    }
    public void Kill() {
        hp = 0;
    }
}
