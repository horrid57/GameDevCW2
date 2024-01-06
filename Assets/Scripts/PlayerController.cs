using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{

    bool hasDoll = false;
    bool hasLight = false;
    bool hasGem = false;
    public bool sageMissionStarted = false;
    public bool hasMap = false;
    int orbsHeld = 0;
    int peopleStolenFrom = 0;
    int peopleHelped = 0;

    public float speed = 10;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Light2D globalLight;

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
        if (other.CompareTag("Cave")) {
            GetComponent<Light2D>().enabled = true;
            globalLight.enabled = false;
        }
        if (other.CompareTag("Gem")) {
            hasGem = true;
            other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Cave")) {
            GetComponent<Light2D>().enabled = false;
            globalLight.enabled = true;
        }
    }



    public void GiveLight() {
        hasLight = true;
        peopleHelped += 1;
        GetComponent<Light2D>().pointLightOuterRadius = 3.5f;
    }

    public void TakeLight() {
        hasLight = false;
        GetComponent<Light2D>().pointLightOuterRadius = 1;
    }


    public bool HasDoll() {
        return hasDoll;
    }
    public void GiveDoll() {
        if (hasDoll) {
            hasDoll = false;
            peopleHelped += 1;
            hasMap = true;
            if (sageMissionStarted) {
                FindFirstObjectByType<OrbManager>().StartWaypoints();
            }
        }
    }

    public int OrbsHeld() {
        return orbsHeld;
    }

    public bool HasGem() {
        return hasGem;
    }

    public void GiveGem() {
        if (hasGem) {
            hasGem = false;
            peopleHelped += 1;
        }
    }

    public void AddMoney(int value) {
        peopleStolenFrom += 1;
    }

    public int GetPeopleHelped() {
        return peopleHelped;
    }

    public int getPeopleStolenFrom() {
        return peopleStolenFrom;
    }
}
