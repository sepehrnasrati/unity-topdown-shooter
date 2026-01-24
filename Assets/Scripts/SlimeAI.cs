using UnityEngine;

public class SlimeAI : MonoBehaviour
{
    public float moveSpeed = 2f;

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector2 direction = ((Vector2)player.position - rb.position).normalized;

        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }
}
