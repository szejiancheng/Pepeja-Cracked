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
    public IngameFirebaseManager firebaseManager;

    private void OnEnable() {
        StartCoroutine(UpdateData());
        
    }

    IEnumerator UpdateData()
    {
        yield return new WaitForSeconds(1);

        firebaseManager.SaveData();

        
        pointsText.text = "You attained a combat score of " + GameMasterScript.GetInstance().CombatScore + ", good work pilot " + firebaseManager.Username + ".";
    }
    private void Update() {
        
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
