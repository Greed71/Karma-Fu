using System.Collections;
using UnityEngine;

public class EnemyCombatAI : MonoBehaviour
{
    GameObject player;
    public Animator controller;
    Animator playerController;
    public Collider2D attackCollider;
    PlayerBehaviour playerBehaviourM;
    public bool hasAttacked = false;
    public float timeBetweenAttacks = 1f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<Animator>();
        playerBehaviourM = player.GetComponent<PlayerBehaviour>();
    }

    void FixedUpdate()
    {
        Collider2D[] hitObjects = new Collider2D[1]; 
        Physics2D.OverlapCollider(attackCollider, new ContactFilter2D(), hitObjects);
        if (hitObjects[0] != null && !hasAttacked)
        {
            if (hitObjects[0].CompareTag("Player"))
            {
                switch (PlayerPrefs.GetInt("Difficulty"))
                {
                    case 0:
                        playerBehaviourM = hitObjects[0].GetComponent<PlayerBehaviour>();
                        Attack(5);
                        break;
                    case 1:
                        playerBehaviourM = hitObjects[0].GetComponent<PlayerBehaviour>();
                        Attack(10);
                        break;
                    case 2:
                        playerBehaviourM = hitObjects[0].GetComponent<PlayerBehaviour>();
                        Attack(15);
                        break;
                }
            }
        }
    }

    public void Attack(int damage){ 
        hasAttacked = true;
        playerBehaviourM.TakeDamage(damage);
        playerController.SetTrigger("Hurt");
        StartCoroutine(ResetHurt());
    }

    IEnumerator ResetHurt()
    {
        yield return new WaitForSeconds(1);
    }

    public void ResetAttack()
    {
        StartCoroutine(ResetAttackAttack());
    }

    IEnumerator ResetAttackAttack()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        hasAttacked = false;
    }
}
