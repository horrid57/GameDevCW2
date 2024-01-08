using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldManDialogue : MonoBehaviour
{
    int interactionNumber = 0;
    int stage = 0; // 0 - before doll found // 1 - giving doll // 2 - after doll given 
    
    private void OnTriggerStay2D(Collider2D other) {
        if (!other.CompareTag("Player")) {
            return;
        }
        FindFirstObjectByType<Prompt>().PromptTalk();
        if (Input.GetKey(KeyCode.E)) {
            // Go to stage 1 if player has all orbs in inventory and first time approaching with all of them
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null && playerController.OrbsHeld() == 3) {
                if (stage == 0) {
                    stage = 1;
                }
            }

            if (stage == 0) {
                interactionNumber += 1;
                if (interactionNumber == 1) {
                    GetComponent<DialogueTrigger>().TriggerDialogue(0);
                    playerController.sageMissionStarted = true;
                    if (playerController.hasMap) {
                        FindFirstObjectByType<OrbManager>().StartWaypoints();
                    }
                }
                else {
                    GetComponent<DialogueTrigger>().TriggerDialogue(1);
                }
            }
            else if (stage == 1) {
                GetComponent<DialogueTrigger>().TriggerDialogue(2);
                stage = 2;
                playerController.GiveLight();
            }
            else {
                GetComponent<DialogueTrigger>().TriggerDialogue(3);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        FindFirstObjectByType<Prompt>().HidePrompt();
    }
}
