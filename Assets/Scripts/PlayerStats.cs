using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : CharacterStats
{
    public override void Die()
    {
        base.Die();
        GameObject.Find("GameTimer").GetComponent<GameTimer>().StopTimer();

        SceneManager.LoadScene(2); // 2 is game over screen
    }
}
