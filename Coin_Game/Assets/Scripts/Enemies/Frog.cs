using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{

    private float horizontalInput;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    [SerializeField] private float idleTime = 2f;
    private float currentIdleTime = 0;
    public bool isIdle = true;
    [SerializeField] private LayerMask groundLayer;

    public bool facingRigth = false;
    public bool isjumping = false;
    public bool isfalling = false;

    public float jumpForceX = 2f;
    public float jumpForceY = 4f;
    public float lastYPos = 0;
    public int direction;
    SpriteRenderer spriteRenderer;

    public enum Animations
    {
        Idle = 0,
        Jumping = 1,
        Falling = 2
    }

    public Animations currentAnim;


    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        lastYPos = transform.position.x;
    }

    private void Update()
    {
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(4, 4, 4);
        }
        //Flip the player to left
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-4, 4, 4);
        }
    }

        private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private void FixedUpdate()
    {
        if (isIdle)
        {
            currentIdleTime += Time.deltaTime;
            if (currentIdleTime >= idleTime)
            {
                currentIdleTime = 0;
                facingRigth = !facingRigth;
                spriteRenderer.flipX = facingRigth;
                Jump();

            }
        }

        if (isGrounded() && !isjumping)
        {
            isIdle = true;
            isjumping = false;
            isfalling = false;
        }
        else if (transform.position.y > lastYPos && !isGrounded() && !isIdle)
        {
            isjumping = true;
            isfalling = false;
        }
        else if (transform.position.y < lastYPos && !isGrounded() && !isIdle)
        {
            isjumping = false;
            isfalling = true;
        }

        lastYPos = transform.position.y;
    }

    private void Jump()
    {
        isjumping = true;
        isIdle = false;
        //int direction = 0;
        if (facingRigth == true)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        body.velocity = new Vector2(jumpForceX * direction, jumpForceY);
        anim.SetBool("jump", true);
        body.gravityScale = 0.5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            body.velocity = new Vector2(body.velocity.x, 0);
            body.gravityScale = 0;
        }
    }

    /*void ChangeAnimation(Animations newAnim)
    {
        if (currentAnim != newAnim)
        {
            anim.SetInteger("state", (int)newAnim);
        }
    }*/

}