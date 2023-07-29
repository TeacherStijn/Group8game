using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public static System.Action onDestroyed;

    public override void Die()
    {
        onDestroyed?.Invoke();
        base.Die();
    }
}
