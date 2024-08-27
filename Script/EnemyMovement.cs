using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    public int jumpForce = 5;
    private Rigidbody2D rb;
    private Animator controller;
    private bool _wasMovingRight = false;
    private bool isGrounded = false;
    public PlayerControls controls;
    PauseMenu2 pauseMenu;
    EnemyCombat enemyCombat;
    private SpriteRenderer spriteRenderer;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<Animator>();
        pauseMenu = FindObjectOfType<PauseMenu2>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyCombat = GetComponent<EnemyCombat>();
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Jump.performed += ctx => { if (isGrounded) Jump(); };
        controls.Gameplay.Punch.performed += ctx => {if(!enemyCombat.isAttacking)enemyCombat.Attack();};
        controls.Gameplay.Pause.performed += ctx => pauseMenu.Pause();
        controls.Gameplay.Navigate.performed += ctx => pauseMenu.Navigate(ctx.ReadValue<Vector2>());
        controls.Gameplay.Select.performed += ctx => {if(GameManager.Instance.gameIsPaused == true)pauseMenu.Select();};
    }

    void Update()
    {
        if (!GameManager.Instance.gameIsPaused){              
            Vector2 move = controls.Gameplay.Movement.ReadValue<Vector2>();
            MovementInput(move.x);
            controller.SetBool("Walking", move.x != 0);
            isGrounded = CheckGrounded(); // Controlla se il personaggio è a terra
        }      
    }

    void MovementInput(float moveHorizontal)
    {
        // Processa il movimento orizzontale basato sull'input memorizzato
        if (moveHorizontal != 0)
        {
            Move(moveHorizontal * speed);
        }
        else
        {
            controller.SetBool("Walking", false);
        }
    }

    private void Move(float speed)
{
    rb.velocity = new Vector2(speed, rb.velocity.y);
    controller.SetBool("Walking", true);

    // Determina se il personaggio è rivolto a sinistra o a destra in base alla velocità.
    bool isMovingRight = speed > 0;

    // Capovolgi l'intero GameObject se la direzione di movimento è cambiata.
    if (_wasMovingRight != isMovingRight)
    {
        _wasMovingRight = isMovingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}


    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        if(controller.name == "PlayerController")
            controller.SetTrigger("Jump");
    }

    bool CheckGrounded()
    {
        // Raycast verso il basso per controllare se il personaggio è a contatto con il terreno
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        if (hit.collider != null)
        {
            return true; // Il personaggio è a terra
        }
        return false; // Il personaggio non è a terra
    }
}
