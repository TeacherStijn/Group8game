using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider.CompareTag("Player"))
        {
            // Make the player drop it's shit
            Debug.Log("Hit!");
        }
    }
}
