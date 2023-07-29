using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem hitEffect;

    [Tooltip("Projectile speed in units per second")]
    public float speed = 10f;

    [Tooltip("The object will be destroyed after this many seconds")]
    public float lifetime = 10f;

    public float damage = 10f;

    [Tooltip("Rotate the bullet in the direction of its initial velocity")]
    public bool faceForward = true;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        rb.velocity *= speed / rb.velocity.magnitude;

        if (faceForward)
        {
            // x/y assumes the base bullet sprite is facing up; y/x assumes it's facing right
            rb.rotation = -Mathf.Atan2(rb.velocity.x, rb.velocity.y) * Mathf.Rad2Deg;
            
            // Make sure the object's rotation is right from the frame it loads
            transform.rotation = Quaternion.Euler(0, 0, rb.rotation);
        }
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isHittable = other.CompareTag("Player") || other.CompareTag("Enemy");
        if (isHittable && !CompareTag(other.tag))
        {
            if (hitEffect)
            {
                Vector3 hitPosition = GetComponent<Collider2D>().ClosestPoint(other.transform.position);
                var particleSystem = Instantiate(hitEffect, hitPosition, transform.rotation);

                // Emit particle manually to avoid delay, because particle `Start Delay` doesn't do its job
                particleSystem.Emit(Vector3.zero, Vector3.zero, 1, 0.25f, Color.white);
            }

            CharacterStats stats = other.GetComponent<CharacterStats>();
            if (stats)
            {
                stats.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}
