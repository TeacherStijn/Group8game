using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ProximityDialogueTrigger : DialogueTrigger
{
    private Transform target;
    private Collider2D trigger;

    private void Start()
    {
        // Set the player as target
        target = PlayerManager.Player.transform;

        trigger = GetComponent<Collider2D>();
        // Make sure the collider is set up correctly
        trigger.isTrigger = true;
        trigger.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == target)
        {
            StartDialogue(); // Start dialogue defined in base class
            trigger.enabled = false;
        }
    }
}
