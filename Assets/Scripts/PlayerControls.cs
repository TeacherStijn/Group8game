using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour
{
    public float moveSpeed = 5f;

    public float dashSpeed = 25f;

    public float dashDuration = 0.19f;

    public int maxDashUses = 3;

    public Animator animator;

    private bool facingRight = true;


    [Tooltip("Separate cooldown time for each dash")]
    public float dashCooldown = 3f;

    private Rigidbody2D rb;
    private bool isDashing = false;
    private int dashUsesRemaining;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashUsesRemaining = maxDashUses;
    }

    private void Update()
    {
        if (DialoguePlayer.instance.isPlayingDialogue)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        movement.Normalize(); // Normalize the movement vector to ensure consistent movement speed

        if (!isDashing)
        {
            rb.velocity = movement * moveSpeed;
        }

        if (moveHorizontal == 0 && moveVertical == 0) //play idle animation if the character is not moving
        {
            animator.SetBool("isRunning", false);
        }
        else //play run animation
        {
            animator.SetBool("isRunning", true);
        }

        if (moveHorizontal > 0 && !facingRight) //calls flipcharacter method
        {
            FlipCharacter();
        }
        if (moveHorizontal < 0 && facingRight)
        {
            FlipCharacter();
        }

        // Check for dash input
        if (Input.GetButtonDown("Dash") && dashUsesRemaining > 0 && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }


    private void FlipCharacter() //flips character if they are facing wrong way
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        
        facingRight = !facingRight;
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        dashUsesRemaining--;

        // Store the current velocity as the dash direction
        Vector2 dashDirection = rb.velocity.normalized;

        // Set the player's velocity to the dash direction
        rb.velocity = dashDirection * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;

        // Refill dash use
        StartCoroutine(RefillDashUse());
    }

    private IEnumerator RefillDashUse()
    {
        yield return new WaitForSeconds(dashCooldown);

        dashUsesRemaining++;
    }
}