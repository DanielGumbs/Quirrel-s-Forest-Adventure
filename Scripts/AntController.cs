using UnityEngine;

public class AntController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float detectionRange = 8f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Transform player;
    private SquirrelMovement playerController;

    private bool facingRight = false;
    private bool isFollowingPlayer = false;

    private float lastCollisionTime;
    public float collisionCooldown = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = player.GetComponent<SquirrelMovement>();
        lastCollisionTime = -collisionCooldown;
    }

    void Update()
    {
        DetectPlayer();

        if (isFollowingPlayer)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        // Flip the ant to face the player direction
        if (direction.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && facingRight)
        {
            Flip();
        }
    }

    void DetectPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            isFollowingPlayer = true;
        }
        else
        {
            isFollowingPlayer = false;
            rb.velocity = Vector2.zero; // Stop moving when player is out of range
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
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time >= lastCollisionTime + collisionCooldown)
            {
                playerController.DieAndRespawn();
                lastCollisionTime = Time.time; // Update last collision time
            }
        }
    }
}
