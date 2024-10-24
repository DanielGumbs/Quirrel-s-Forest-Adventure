
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private float wallJumpCooldown;
    private float horizontalInput;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float hurtForce = 10f;
    [SerializeField] private GameObject gameOut;
    private Health currentHealth;

    [SerializeField] public float speed;
    [SerializeField] public float speedPower;

    [SerializeField] private Text cherryText;
    private int coins = 0;
    // private Action state;

    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        currentHealth = GetComponent<Health>();
        Debug.Log(currentHealth.currentHealth);

        speed = 10;
        speedPower = 25;
    }

    private void Update()
    {

        //Bepaalt de richting
        horizontalInput = Input.GetAxis("Horizontal");

        //Beweegt de speler
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        Crouch();
        Falling();
        Flip();
        UpdateCherriesText();

        if (isGrounded())
        {
            anim.SetBool("idle", true);
            anim.SetBool("falling", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
    }

    /*private void Walljump()
    {
        if (wallJumpCooldown > 0.2f)
        {
        }
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (onWall() && !isGrounded())
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 7;
        }

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
        {
            Jump();
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }
    }*/

    private void Flip()
    {
        //Flip the player to right 
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(5, 5, 5);
        }
        //Flip the player to left
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-5, 5, 5);
        }
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, speedPower);
            //Start animation springen
            anim.SetTrigger("jump");
        }
    }
    private void Falling()
    {
        if (!isGrounded() && body.velocity.y <= -2.0f)
        {
            //Start animatie vallen
            anim.SetBool("fall", true);
        }
        else
        {
            //Stop animatie vallen
            anim.SetBool("fall", false);
        }
    }

    private void Crouch()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetBool("crouch", true);
        }
        else
        {
            anim.SetBool("crouch", false);
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (!isGrounded() && body.velocity.y < 0)
            {
                collision.gameObject.SetActive(false);
            }
            else
            {
                if (collision.gameObject.transform.position.x > transform.position.x)
                {
                    currentHealth.TakeDamage(1);
                }
                else
                {
                    body.velocity = new Vector2(-hurtForce, body.velocity.y);
                    currentHealth.TakeDamage(1);

                }
            }
        }

        if (collision.tag == "Fall")
        {
            Debug.Log("fall");
            currentHealth.TakeDamage(3);
        }

        if (collision.tag == "Coin")
        {
            collision.gameObject.SetActive(false);
            coins++;
        }

        if (collision.tag == "Speed")
        {
            speed = 20;

        }

        if (collision.tag == "Jumpspeed")
        {
            speedPower = 40;

        }

        if (collision.tag == "1")
        {
            SceneManager.LoadScene(2);

        }

        if (collision.tag == "2")
        {
            SceneManager.LoadScene(3);
        }

        if (collision.tag == "3")
        {
            gameOut.SetActive(true);
        }

    }

    private void UpdateCherriesText()
    {
        cherryText.text = " : " + coins.ToString() + "x";
    }

}
