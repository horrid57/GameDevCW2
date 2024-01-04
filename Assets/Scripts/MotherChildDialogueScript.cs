using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherChildDialogueScript : MonoBehaviour
{
    int interactionNumber = 0;
    int stage = 0; // 0 - before doll found // 1 - giving doll // 2 - after doll given 
    
    private void OnTriggerEnter2D(Collider2D other) {
        // Go to stage 1 if player has doll in inventory
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
        if (playerController != null && playerController.HasDoll()) {
            stage = 1;
            playerController.GiveDoll();
        }

        if (stage == 0) {
            interactionNumber += 1;
            if (interactionNumber == 1) {
                GetComponent<DialogueTrigger>().TriggerDialogue(0);
            }
            else if (interactionNumber <= 3) {
                GetComponent<DialogueTrigger>().TriggerDialogue(1);
            }
            else {
                GetComponent<DialogueTrigger>().TriggerDialogue(2);
            }
        }
        else if (stage == 1) {
            GetComponent<DialogueTrigger>().TriggerDialogue(3);
            stage = 2;
        }
        else {
            GetComponent<DialogueTrigger>().TriggerDialogue(4);
        }
    }
}
