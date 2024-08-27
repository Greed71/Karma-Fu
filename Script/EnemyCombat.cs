using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Animator controller;
    public Collider2D attackCollider;
    public PlayerBehaviour playerBehaviourM, playerBehaviourF;
    private bool hasAttacked = false;
    public bool isAttacking = false;
    public GameObject playerM, playerF;

    void Update()
    {
        Collider2D[] hitObjects = new Collider2D[1]; 
        Physics2D.OverlapCollider(attackCollider, new ContactFilter2D(), hitObjects);
        if (hitObjects[0] != null && !hasAttacked)
        {
            if (hitObjects[0].CompareTag("Player") && playerM.activeSelf)
            {
                playerBehaviourM = hitObjects[0].GetComponent<PlayerBehaviour>();
                playerBehaviourM.TakeDamage(10);
                hasAttacked = true;
            }
            else if (hitObjects[0].CompareTag("Player") && playerF.activeSelf)
            {
                playerBehaviourF = hitObjects[0].GetComponent<PlayerBehaviour>();
                playerBehaviourF.TakeDamage(10);
                hasAttacked = true;
            }
        }
    }

    public void Attack()
    {
        isAttacking = true;
        controller.SetTrigger("Kick");
        attackCollider.enabled = true;
        hasAttacked = false;
        StartCoroutine(ResetAttack());
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(controller.GetCurrentAnimatorStateInfo(0).length);
        isAttacking = false;
    }
}
