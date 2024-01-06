using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderTriggerEnterDialogue : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player")) {
            return;
        }
        GetComponent<DialogueTrigger>().TriggerDialogue();
    }
}
