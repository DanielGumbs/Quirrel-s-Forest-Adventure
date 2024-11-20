using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SquirrelMovement : MonoBehaviour
{
    public float moveSpeed = 0.2f;
    public float jumpForce = 5f;
    public int extraPoints = 0;

    public TMP_Text pointsText;
    public TMP_Text timerText;
    public TMP_Text finishStatsText;
    public TMP_Text gameOverText;
    public TMP_Text gameWinText;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool facingRight = true;
    private bool isDead = false;
    private bool isTimerRunning = true; // Flag to control the timer
    private const float fallThreshold = -4f;
    private float startTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        UpdatepointsText();
        startTime = Time.time;
        if (gameOverText != null) gameOverText.gameObject.SetActive(false);
        if (gameWinText != null) gameWinText.gameObject.SetActive(false);
        if (finishStatsText != null) finishStatsText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isDead) return;

        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput > 0 && !facingRight) Flip();
        else if (moveInput < 0 && facingRight) Flip();

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
        }

        if (transform.position.y < fallThreshold)
        {
            DieAndRespawn();
        }

        if (timerText != null && isTimerRunning)
        {
            float elapsedTime = Time.time - startTime;
            timerText.text = elapsedTime.ToString("F2");
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
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Acorn"))
        {
            extraPoints++;
            UpdatepointsText();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("finishTree"))
        {
            isTimerRunning = false;
            float elapsedTime = Time.time - startTime;
            if (gameWinText != null) gameWinText.gameObject.SetActive(true);
            if (finishStatsText != null)
            {
                finishStatsText.gameObject.SetActive(true);
                finishStatsText.text = $"Time: {elapsedTime:F2}\nAcorns: {extraPoints}";
            }
            StartCoroutine(ReloadSceneAfterDelay(6f));
        }
    }

    public void DieAndRespawn()
    {
        isDead = true;
        rb.velocity = Vector2.zero;
        if (gameOverText != null) gameOverText.gameObject.SetActive(true);
        StartCoroutine(ReloadSceneAfterDelay(3f));
    }

    void UpdatepointsText()
    {
        pointsText.text = extraPoints.ToString();
    }

    private IEnumerator ReloadSceneAfterDelay(float delay)
    {
        GetComponent<SpriteRenderer>().enabled = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
