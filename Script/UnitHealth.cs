using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth
{
    public float health;
    public float maxHealth;
    public UnitHealth(float health, float maxHealth)
    {
        this.health = health;
        this.maxHealth = maxHealth;

    }

    public void Damage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
    }

    public void Heal(float heal)
    {
        health += heal;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
