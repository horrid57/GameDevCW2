using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue[] dialogue;


    /*

    Player #0 - picking up the kid's doll

    Mother #0 - Explains about the king and how they are out of money

    Woman #0 - first polite encounter to explain lost doll
    Woman #1 - summary dialogue
    Woman #2 - irritated that player keeps approaching so very short message
    Woman #3 - Doll is returned, woman is thankful, gives map segment that daughter found in return
    Woman #4 - Generic thankful message

    Sage #0 - first polite encounter to explain orbs and incomplete non-essential map
    Sage #1 - "bring the orbs to me" type
    Sage #2 - Thank you message + giving of light orb
    Sage #3 - generic thankful message "go forth in light" or something

    */


    public void TriggerDialogue(int num = 0) {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue[num]);
    }
}
