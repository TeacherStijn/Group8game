using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Tooltip("The object will be destroyed after this many seconds")]
    public float lifetime = 10f;

    [Tooltip("Rotate the bullet in the direction of its initial velocity")]
    public bool faceForward = true;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (faceForward)
        {
            if (rb.velocity.y == 0f)
            {

                rb.rotation = 90f - 180f * Convert.ToSingle(rb.velocity.x > 0f);
            }
            else
            {
                // x/y assumes the base bullet sprite is facing up; y/x assumes it's facing right
                rb.rotation = -Mathf.Atan2(rb.velocity.x, rb.velocity.y) * Mathf.Rad2Deg;
            }
        }

        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("Hit something");
        if (other.gameObject.CompareTag("Player"))
        {
            // Make the player drop it's shit
            Debug.Log("Hit!");
            // PlayerManager.lifeManager.takeDamage()
        }
    }
}
