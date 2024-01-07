using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingDialogue : MonoBehaviour
{    
    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) {
            return;
        }

        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
        
        int peopleHelped = playerController.GetPeopleHelped();
        int peopleStolenFrom = playerController.getPeopleStolenFrom();

        if (peopleStolenFrom > 2) {
            GetComponent<DialogueTrigger>().TriggerDialogue(0);
        }
        else if (peopleStolenFrom > 0) {
            GetComponent<DialogueTrigger>().TriggerDialogue(1);
        }
        else if (peopleHelped < 2) {
            GetComponent<DialogueTrigger>().TriggerDialogue(2);
        }
        else {
            GetComponent<DialogueTrigger>().TriggerDialogue(3);
        }

    }
}
