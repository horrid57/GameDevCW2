using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;

    private float typingSpeed;

    private Queue<Sentence> sentences;

    void Start() {
        sentences = new Queue<Sentence>();
    }

    public void StartDialogue(Dialogue dialogue) {

        animator.SetBool("IsOpen", true);
        Time.timeScale = 0;

        sentences.Clear();

        foreach(Sentence sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }
        Sentence sentence = sentences.Dequeue();
        nameText.text = sentence.name;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(Sentence sentence) {
        dialogueText.text = "";
        foreach(char letter in sentence.text.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(sentence.typingSpeed);
        }
    }

    void EndDialogue() {
        animator.SetBool("IsOpen", false);
        Time.timeScale = 1;
    }
}
