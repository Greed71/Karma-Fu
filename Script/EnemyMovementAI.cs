using UnityEngine;

public class EnemyMovementAI : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public float speed = 2f, attackRange = 1f;
    EnemyFlip enemyFlip;
    EnemyCombatAI enemyCombatAI;
    PlayerCombatSP playerCombat;
    bool isGrounded = false;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyFlip = animator.GetComponent<EnemyFlip>();
        rb = animator.GetComponent<Rigidbody2D>();
        enemyCombatAI = animator.GetComponent<EnemyCombatAI>();
        playerCombat = player.GetComponent<PlayerCombatSP>();
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isGrounded = CheckGrounded(); // Controlla se il personaggio è a terra
        enemyFlip.LookAtPlayer();
        Vector2 target = new(player.position.x + (attackRange - 0.1f) , rb.position.y);
        if(!playerCombat.isHurt && isGrounded)
        {
            Vector2 direction = (target - rb.position).normalized;
            rb.velocity = direction * speed;
        }else{
            rb.velocity = Vector2.zero;
        }
        if(Vector2.Distance(player.position, rb.position) <= attackRange && !enemyCombatAI.hasAttacked)
        {
            animator.SetTrigger("Punch");
            
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.ResetTrigger("Punch");
       enemyCombatAI.ResetAttack();
    }

    bool CheckGrounded()
    {
        // Raycast verso il basso per controllare se il personaggio è a contatto con il terreno
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, 0.1f);
        if (hit.transform != null)
        {
            return true; // Il personaggio è a terra
        }
        return false; // Il personaggio non è a terra
    }
}
