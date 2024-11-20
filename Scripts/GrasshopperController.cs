using UnityEngine;

public class GrasshopperController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float jumpForce = 2f;
    public float horizontalJumpForce = 2f;
    public int maxJumps = 2;

    private Rigidbody2D rb;
    private int jumpCount = 0;
    private Transform player;
    private SquirrelMovement playerController;
    private bool isFollowingPlayer = false;
    private bool facingRight = false;

	private float lastCollisionTime;
	public float collisionCooldown = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = player.GetComponent<SquirrelMovement>();
        lastCollisionTime = -collisionCooldown;

        Jump();
    }

    void Update()
    {
        if (isFollowingPlayer)
        {
            MoveTowardsPlayer();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(-horizontalJumpForce, jumpForce);
        jumpCount++;
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        // Flip the grasshopper to face the direction of movement
        if (direction.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (jumpCount < maxJumps)
            {
                Jump();
            }
            else
            {
                isFollowingPlayer = true;
            }
        }

        if (collision.gameObject.CompareTag("Player"))
        {
			if (Time.time >= lastCollisionTime + collisionCooldown)
            {
            	playerController.DieAndRespawn();
                lastCollisionTime = Time.time;
            }
        }
    }
}
