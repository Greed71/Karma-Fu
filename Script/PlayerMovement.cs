using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IPlayer
{
    public float speed = 2f, jumpForce = 6.5f;
    private Rigidbody2D rb;
    private Animator controller;
    private bool isGrounded = false;
    public bool _wasMovingLeft = false;

    /*public void OnMove(InputAction.CallbackContext context){
        MovementInput(context.ReadValue<float>()); 
    }

    public void OnJump(InputAction.CallbackContext context){
        if(isGrounded && context.performed){
            Jump();
        }
    }*/

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<Animator>();
    }

    void Update()
    {
        if (!GameManager.Instance.gameIsPaused){
            // Movimento arrow
            float moveHorizontal = Input.GetAxisRaw("HorizontalWASD");
            MovementInput(moveHorizontal);
            isGrounded = CheckGrounded();
            // Salto solo se il personaggio è a terra
            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
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

    public void Move(float speed)
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        controller.SetBool("Walking", true);

        // Determina se il personaggio è rivolto a sinistra o a destra in base alla velocità.
        bool isMovingLeft = speed < 0;

        // Capovolgi l'intero GameObject se la direzione di movimento è cambiata.
        if (_wasMovingLeft != isMovingLeft)
        {
            _wasMovingLeft = isMovingLeft;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }


    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        controller.SetTrigger("Jump");
    }

    bool CheckGrounded()
    {
        // Raycast verso il basso per controllare se il personaggio è a contatto con il terreno
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Ground")
            {
                return true; // Il personaggio è a terra
            }
        }
        return false; // Il personaggio non è a terra
    }
}