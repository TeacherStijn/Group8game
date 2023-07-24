using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour
{
    public float moveSpeed = 5f;

    public float dashSpeed = 25f;

    public float dashDuration = 0.19f;

    public int maxDashUses = 3;

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

        // Check for dash input
        if (Input.GetButtonDown("Dash") && dashUsesRemaining > 0 && !isDashing)
        {
            StartCoroutine(Dash());
        }
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