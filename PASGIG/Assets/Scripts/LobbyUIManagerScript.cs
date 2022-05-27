using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUIManagerScript : MonoBehaviour
{
    public static LobbyUIManagerScript instance;

    //Screen object variables
    public GameObject obj1;
    public GameObject obj2;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    //Functions to change the login screen UI
    public void LobbyScreen() //Back button
    {
        obj1.SetActive(true);
        //ClearAllText();
        obj2.SetActive(false);
        
    }
}