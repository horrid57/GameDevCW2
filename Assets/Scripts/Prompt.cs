using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Prompt : MonoBehaviour
{
    public void PromptTalk() {
        GetComponent<TextMeshProUGUI>().text = "E to Talk";
    }

    public void PromptSteal() {
        GetComponent<TextMeshProUGUI>().text = "R to Steal";
    }

    public void HidePrompt() {
        GetComponent<TextMeshProUGUI>().text = "";
    }
}
