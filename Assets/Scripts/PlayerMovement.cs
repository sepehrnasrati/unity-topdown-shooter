using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    private Vector2 inputDirection;
    private Animator anim;
    private SpriteRenderer sr;

    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector2(moveX, moveY).normalized;

        // animation values
        anim.SetBool("IsWalking", inputDirection != Vector2.zero);

        
        anim.SetFloat("MoveX", moveX);
        anim.SetFloat("MoveY", moveY);

        // flip sprite horizontally
        if (moveX != 0)
        {
            sr.flipX = moveX < 0;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + inputDirection * moveSpeed * Time.fixedDeltaTime);
    }
}
