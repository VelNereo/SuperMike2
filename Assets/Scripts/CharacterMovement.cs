using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody2D rb2D;
    public float moveSpeed;
    public float jumpVelocity;
    [Header("Flip")]
    public bool facingLeft = true;
    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    [Range(0.01f,0.5f)] public float groundCheckRadius;
    public bool grounded;
    [Header("Coyote Time")]
    [Range(0.05f,0.5f)] public float coyoteTime = 0.2f;
    public float coyoteTimeCounter;
    [Header("Jump Buffer")]
    [Range(0.1f, 0.5f)]public float jumpBuffer = 0.2f;
    public float jumpBufferCounter;
    
    void Start()
    {
        facingLeft = false;
    }

    void Update()
    {
        var moveDirection = Input.GetAxisRaw("Horizontal");
        rb2D.linearVelocity = new Vector2(moveDirection * moveSpeed, rb2D.linearVelocity.y);
        
        if (grounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBuffer;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpVelocity);
            jumpBufferCounter = 0f;
        }
        if (Input.GetKeyUp(KeyCode.Space) && rb2D.linearVelocity.y > 0)
        {
            coyoteTimeCounter = 0f;
        }

        if (moveDirection > 0 && facingLeft)
        {
            Flip();
        }
        else if (moveDirection < 0 && !facingLeft)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void Flip()
    {
        facingLeft = !facingLeft;
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
