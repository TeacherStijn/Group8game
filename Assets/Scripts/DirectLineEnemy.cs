using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class DirectLineEnemy : Enemy
{
    protected override void Start()
    {
        base.Start();
    }

    public override void Move()
    {
        // Normalized to just get the direction
        Vector3 direction = (player.GetComponent<Transform>().position - transform.position).normalized;

        // Move towards player.
        transform.position += direction * speed * Time.deltaTime;

        // Flip to match sprite movement, assuming the base sprite is facing left
        Vector3 scale = transform.localScale;
        if ((scale.x > 0 && direction.x > 0) || (scale.x < 0 && direction.x < 0))
        {
            scale.x = -scale.x;
        }
        transform.localScale = scale;
    }
}
