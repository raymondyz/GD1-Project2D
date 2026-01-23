using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    float movementX;
    float movementY;
    [SerializeField] float speed = 5.0f;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    bool isGrounded;

    int numAirJumps = 1;
    [SerializeField] int maxAirJumps = 1;

    int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // float movementDistanceX = movementX * speed * Time.deltaTime;
        // float movementDistanceY = movementY * speed * Time.deltaTime;

        // transform.position = new Vector2(transform.position.x + movementDistanceX, transform.position.y + movementDistanceY);
        rb.linearVelocity = new Vector2(movementX * speed, rb.linearVelocity.y);

        if (!Mathf.Approximately(movementX, 0f))
        {
            animator.SetBool("isRunning", true);
            spriteRenderer.flipX = movementX < 0;
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void OnJump()
    {
        if (isGrounded)
        {
            rb.AddForce(new Vector2(0, 300));
            animator.SetBool("isJumping", true);
        }
        else if (numAirJumps > 0)
        {
            rb.AddForce(new Vector2(0, 300));
            numAirJumps --;
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();

        movementX = v.x;
        movementY = v.y;

        // Debug.Log(movementX);
        // Debug.Log(movementY);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectible"))
        {
            score ++;
            collision.gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            numAirJumps = maxAirJumps;
            animator.SetBool("isJumping", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
