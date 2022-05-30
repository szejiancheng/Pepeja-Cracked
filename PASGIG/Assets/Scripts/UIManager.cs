using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //Screen object variables
    public GameObject loginUI;
    public GameObject registerUI;
    public GameObject IntroUI;

    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;
    public TMP_Text warningRegisterText;

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
    public void LoginScreen() //Back button
    {
        loginUI.SetActive(true);
        //ClearAllText();
        registerUI.SetActive(false);
        
    }
    public void RegisterScreen() // Register button
    {
        loginUI.SetActive(false);
        //ClearAllText();
        registerUI.SetActive(true);
    }
    public void IntroScreen()
    {
        loginUI.SetActive(false);
        //ClearAllText();
        IntroUI.SetActive(true);
    }

    public void ClearAllText()
    {
        warningLoginText.text = "";
        //warningRegisterText.text = "";
        confirmLoginText.text = "";
    }
}