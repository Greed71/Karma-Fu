using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public UnitHealth unitHealth;

    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }

    public void SetUnitHealth(UnitHealth unitHealth)
    {
        this.unitHealth = unitHealth;
        SetMaxHealth(unitHealth.maxHealth);
        SetHealth(unitHealth.health);
    }

    public void Update()
    {
        if (unitHealth != null)
        {
            SetHealth(unitHealth.health);
        }
    }
}
