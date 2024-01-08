using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderTriggerEnterDialogue : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other) {
        if (!other.CompareTag("Player")) {
            return;
        }
        FindFirstObjectByType<Prompt>().PromptTalk();
        if (Input.GetKey(KeyCode.E)) {
            GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        FindFirstObjectByType<Prompt>().HidePrompt();
    }
}
