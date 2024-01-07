using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRunner : MonoBehaviour
{

    public void StartGame() {
        SceneManager.LoadScene("MainScene");
    }

    public void ReturnToMenu() {
        SceneManager.LoadScene("Menu");
    }

}
