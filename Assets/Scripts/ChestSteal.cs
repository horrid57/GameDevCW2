using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestSteal : MonoBehaviour
{

    public int value;
    [SerializeField] private GameObject questTrigger;
    

    private void OnTriggerStay2D(Collider2D other) {
        if (!other.CompareTag("Player") || value == 0) {
            return;
        }
        if (Input.GetKey(KeyCode.E)) {
            GetComponent<DialogueTrigger>().TriggerDialogue();
            other.GetComponent<PlayerController>().AddMoney(value);
            value = 0;
            if (questTrigger != null) {
                questTrigger.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag("Player")) {
            return;
        }
        if (value == 0) {
            transform.root.gameObject.SetActive(false);
        }
    }
}
