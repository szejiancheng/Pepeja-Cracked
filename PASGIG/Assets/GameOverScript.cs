using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScript : MonoBehaviour
{
    public TMP_Text pointsText;
    public GameObject gameUI;
    public GameObject gameOverScreen;

    public void InitScore(int score)
    {
        gameOverScreen.SetActive(true);
        pointsText.text = "Score: " + score.ToString();
        gameUI.SetActive(false);
    }

    public void RestartButton() 
    {
        SceneManager.LoadScene("Game");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Lobby");
    }
}
