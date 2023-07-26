using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DirectLineEnemy : Enemy
{
    protected override void Start()
    {
        base.Start();
        this.detectionRadius = 15;
        this.speed = 0.5f;
    }

    public override void Move()
    {
        // Normalized to just get the direction
        Vector3 direction = (player.GetComponent<Transform>().position - transform.position).normalized;

        // Move towards player.
        transform.position += direction * speed * Time.deltaTime;
    }
}
