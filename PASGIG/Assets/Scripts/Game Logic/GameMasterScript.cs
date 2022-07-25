using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterScript : MonoBehaviour
{
    static GameMasterScript instance;
    public bool isPaused = false;

    public int CombatScore = 0;
    public int LivesLeft = 3;

    public PlayerSpawner playerSpawner;
    public GameObject GameOverScreen;
    

    public static GameMasterScript GetInstance() 
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying GameMaster!");
            Destroy(this);
        }
    }

    private void Start() {
        SetFramerate();
    }

    public void RespawnPlayer()
    {
        
        if (LivesLeft > 0) {
            LivesLeft --;
            StartCoroutine(playerSpawner.SpawnPlayer());
        } else {
            GameOverScreen.SetActive(true);
            Debug.Log("Uploading combat score to the cloud");
        }
    }

    public void AddScore(int score)
    {
        CombatScore += score;
    }

    public void Pause () {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Unpause ()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }

    void GameStart()
    {
        //Assign player position, spawn player in
        //Unfreeze player position, start enemy spawner
        //
    }

    void GameOver()
    {
        //Disable control UI, bring up GameOver UI
        //Disable enemy spawner

    }

    void SetFramerate()
    {
        Application.targetFrameRate = 30;
    }
}
