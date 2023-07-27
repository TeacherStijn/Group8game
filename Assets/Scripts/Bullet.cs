using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log("Hit something");
        if (other.gameObject.CompareTag("Player"))
        {
            // Make the player drop it's shit
            Debug.Log("Hit!");
            // PlayerManager.lifeManager.takeDamage()
        }
    }
}
