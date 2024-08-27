using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;
    [SerializeField]Flash flash;

    public void TakeDamage(float damage)
    {
        GameManager.Instance.playerHealth.Damage(damage);
        healthBar.SetHealth(GameManager.Instance.playerHealth.health);
        flash.Flashh("Enemy");
    }
}
