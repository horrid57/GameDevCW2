using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerDialogueTrigger : MonoBehaviour
{
    int interactionNumber = 0;
    bool gemTaken = false;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) {
            return;
        }
        if (!gemTaken) {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController != null && playerController.HasGem()) {
                gemTaken = true;
            }
        }

        interactionNumber += 1;
        if (interactionNumber == 1) {
            if (!gemTaken) {
                GetComponent<DialogueTrigger>().TriggerDialogue(0);
            }
            else {
                GetComponent<DialogueTrigger>().TriggerDialogue(1);
            }
        }
        else {
            if (!gemTaken) {
                GetComponent<DialogueTrigger>().TriggerDialogue(2);
            }
            else {
                GetComponent<DialogueTrigger>().TriggerDialogue(3);
            }
        }
    }
}
