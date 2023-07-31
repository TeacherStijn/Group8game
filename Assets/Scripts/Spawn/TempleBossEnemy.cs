using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TempleBossEnemy : Enemy
{
    public Weapon weaponReward;

    private bool hasDied = false;

    protected override void Start()
    {
        base.Start();
        isWaiting = true;
    }

    public override void Move()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer > detectionRadius / 2)
        {
            if (isWaiting)
            {
                health = maxHealth;
            }
            return;
        }

        isWaiting = false;

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

    public override void Die()
    {
        if (hasDied)
        {
            return;
        }

        hasDied = true;

        PlayerManager.AddCrystal();
        PlayerManager.RestoreHealth(10f);
        if (weaponReward)
        {
            PlayerManager.AddWeapon(weaponReward);
        }
        base.Die();
    }
}
