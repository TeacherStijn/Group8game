using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDragon : DirectLineEnemy
{
    public float maxMoveDistance;
    private Vector3 startingPosition;

    protected override void Start()
    {
        startingPosition = transform.position;
        maxMoveDistance = 3f;
    }

    public override void Move()
    {
        // Normalized to just get the direction
        Vector3 direction = (player.GetComponent<Transform>().position - transform.position).normalized;

        // Calculate the new position
        Vector3 newPos = transform.position + direction * speed * Time.deltaTime;

        // Don't move if the new position is too far from the starting position
        if (Vector3.Distance(newPos, startingPosition) > maxMoveDistance)
        {
            return;
        }

        // Otherwise, proceed with the move
        transform.position = newPos;

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
        Debug.Log("You win!");
        // Maybe use the order of scenes instead of stringname?
        GameObject.Find("GameTimer").GetComponent<GameTimer>().StopTimer();
        SceneManager.LoadScene("WinScreen");
    }
}
