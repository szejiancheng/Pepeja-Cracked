using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject GameUI;

    public void OpenMenu()
    {
        pauseMenuUI.SetActive(true);
        GameUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void CloseMenu()
    {
        pauseMenuUI.SetActive(false);
        GameUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void QuitGame()
    {
        CloseMenu();
        SceneManager.LoadScene("Lobby");
    }
}
