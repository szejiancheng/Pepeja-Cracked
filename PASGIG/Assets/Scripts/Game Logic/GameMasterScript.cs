using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMasterScript : MonoBehaviour
{
    static GameMasterScript instance;

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
