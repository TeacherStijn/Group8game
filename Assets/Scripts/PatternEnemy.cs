using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternEnemy : Enemy
{
    public float xSpeed = 20.0f;
    public float ySpeed = 20.0f;
    public float xOffsetSpeed = 5.0f;
    public float yOffsetSpeed = 5.0f;
    private Vector3 pos;

    protected override void Start()
    {
        base.Start();
        pos = transform.position;
    }

    public override void Move()
    {
        // Check the distance to the player.
        float distanceToPlayer = Vector3.Distance(player.GetComponent<Transform>().position, transform.position);

        // If the player is within the detection radius, move towards the player.
        if (distanceToPlayer <= detectionRadius)
        {
            Vector3 direction = (player.GetComponent<Transform>().position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            // Circular / round movement idea
            float x = Mathf.Cos(Time.time * xOffsetSpeed) * xSpeed;
            float y = Mathf.Sin(Time.time * yOffsetSpeed) * ySpeed;
            Vector3 offset = new Vector3(x, y, 0);
            pos += transform.right * Time.deltaTime * speed;
            transform.position = pos + offset;
        }
    }
}
