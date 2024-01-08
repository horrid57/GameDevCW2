using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    public GameObject ending1Panel; // Knight ending
    public GameObject ending2Panel; // Help Ending
    public GameObject ending3Panel; // Prison Ending
    public GameObject ending4Panel; // Game Over - Player Died
    public GameObject ending5Panel; // Player didn't help

    void Start()
    {
        // Hide all ending panels at the start
        ending1Panel.SetActive(false);
        ending2Panel.SetActive(false);
        ending3Panel.SetActive(false);
        ending4Panel.SetActive(false);
        ending5Panel.SetActive(false);
    }

    public void ShowEnding(int endingNumber)
    {

        switch (endingNumber)
        {
            case 1:
                ending1Panel.SetActive(true);
                break;
            case 2:
                ending2Panel.SetActive(true);
                break;
            case 3:
                ending3Panel.SetActive(true);
                break;
            case 4:
                ending4Panel.SetActive(true);
                break;
            case 5:
                ending4Panel.SetActive(true);
                break;
        }

    }


    //public void RestartGame()
    //{
    //    Time.timeScale = 1; // Reset time scale
    //    SceneManager.LoadScene("YourGameScene");
    //}

    //public void ReturnToMainMenu()
    //{
    //    Time.timeScale = 1; // Reset time scale
    //    SceneManager.LoadScene("MainMenu");
    //}
}

