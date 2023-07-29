using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool disappearAfterDialogue = false;

    public void StartDialogue()
    {
        DialoguePlayer.instance.OpenDialogue(dialogue);

        if (disappearAfterDialogue)
        {
            gameObject.SetActive(false);
        }
    }
}
