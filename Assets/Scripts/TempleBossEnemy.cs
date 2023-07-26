using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TempleBossEnemy : Enemy
{
    protected override void Start()
    {
        base.Start();
        this.detectionRadius = 3f;
        this.speed = 0.5f;
        Debug.Log("Im alive!");
    }

    public override void Move()
    {
        // Normalized to just get the direction
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}
