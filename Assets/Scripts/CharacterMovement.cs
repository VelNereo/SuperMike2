using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public float moveSpeed;
    public float jumpVelocity;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius;
    public bool grounded;
    void Start()
    {
        
    }

    void Update()
    {
        var moveDirection = Input.GetAxisRaw("Horizontal");
        rb2D.linearVelocity = new Vector2(moveDirection * moveSpeed, rb2D.linearVelocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, jumpVelocity);
        }
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
}
