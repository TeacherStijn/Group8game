using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected AudioSource shootingSound;
    protected InventoryManager inventoryManager;

    public abstract void Fire(Vector3 target);

    protected virtual void Start()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();
        shootingSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "" && collision.gameObject.tag != null)
        {
            // Check if player is hit with this, but the owner of the gun isn't the Player.
            if (collision.gameObject.CompareTag("Player") && !gameObject.CompareTag("Player")) { 
                Debug.Log("Got a new gun!");
                inventoryManager.AddItem(this.gameObject);
            }
        }
    }
}