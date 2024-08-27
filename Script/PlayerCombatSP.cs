using System.Collections;
using UnityEngine;

public class PlayerCombatSP : MonoBehaviour
{
    GameObject enemy;
    public Animator controller;
    Animator enemyController;
    public Collider2D attackCollider;
    EnemyBehaviour enemyBehaviour;
    private bool isAttacking = false;
    public bool hasAttacked = false, isHurt = false;
    public float timeBetweenAttacks = 1f;

    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyController = enemy.GetComponent<Animator>();
        enemyBehaviour = enemy.GetComponent<EnemyBehaviour>();
    }

    void Update()
    {
        Collider2D[] hitObjects = new Collider2D[1]; 
        Physics2D.OverlapCollider(attackCollider, new ContactFilter2D(), hitObjects);
        if (hitObjects[0] != null && !hasAttacked)
        {
            if (hitObjects[0].CompareTag("Enemy"))
            {
                enemyBehaviour = hitObjects[0].GetComponent<EnemyBehaviour>();
                enemyBehaviour.TakeDamage(10);
                hasAttacked = true;
                enemyController.SetTrigger("Hurt");
                isHurt = true;
                StartCoroutine(ResetHurt());
            }
        }
        if (!GameManager.Instance.gameIsPaused){
            if (Input.GetMouseButtonDown(0) && !isAttacking)
            {
                Attack();
                hasAttacked = false;
            }
        }
    }

    public void Attack()
    {
        isAttacking = true;
        controller.SetTrigger("Kick");
        attackCollider.enabled = true;
        StartCoroutine(ResetAttack());
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
    }

    IEnumerator ResetHurt()
    {
        yield return new WaitForSeconds(1);
        isHurt = false;
    }
}
