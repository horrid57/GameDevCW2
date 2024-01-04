using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    bool hasDoll = false;
    int orbsHeld = 0;

    public float speed = 10;

    [SerializeField] private Rigidbody2D rb;

    private void Update() {
        Vector2 directionVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (directionVector.magnitude > 1) {
            directionVector.Normalize();
        }
        rb.velocity = directionVector * speed;
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Doll")) {
            GetComponent<DialogueTrigger>().TriggerDialogue(0); // picking up doll message
            other.gameObject.SetActive(false);
            hasDoll = true;
        }
        if (other.CompareTag("Orb")) {
            other.gameObject.SetActive(false);
            orbsHeld += 1;
        }
    }


    public bool HasDoll() {
        return hasDoll;
    }
    public void GiveDoll() {
        hasDoll = false;
    }

    public int OrbsHeld() {
        return orbsHeld;
    }
}
