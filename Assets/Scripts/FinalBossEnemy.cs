using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalBossEnemy : TempleBossEnemy
{
    public override void Die()
    {
        SceneManager.LoadScene(3);
        base.Die();
    }
}
