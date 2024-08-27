using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;
    [SerializeField] Flash flash;

    public void TakeDamage(float damage)
    {
        GameManager.Instance.enemyHealth.Damage(damage);
        healthBar.SetHealth(GameManager.Instance.enemyHealth.health);
        flash.Flashh("Player");
    }
}
