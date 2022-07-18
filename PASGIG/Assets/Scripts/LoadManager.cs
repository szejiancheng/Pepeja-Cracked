
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using TMPro;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{   

    static LoadManager instance;

    public static LoadManager GetInstance() 
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
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    private void OnDestroy() 
    {
        Debug.Log("LoadManager destroyed");
        
    }

    public bool FirstTimeLogin = false;
    public string Username;
    public string GamesPlayed;
    public string GamesWon;
    public string HighScore;
    private DatabaseReference DBreference;
    private Firebase.Auth.FirebaseAuth auth;
    private FirebaseUser User;

    [Header("Basic Field References")]
    public TMP_Text usernameTextField;
    public TMP_Text emailTextField;
    public TMP_Text GamesPlayedField;
    public TMP_Text GamesWonField;
    public TMP_Text HighScoreField;


    void Start()
    {
        Debug.Log("Loading user data");
        User = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        Debug.Log("Fetching user data");
        StartCoroutine(LoadUserData());
        

        /*
        if (AuthManager.instance).user != null)
        {
            StartCoroutine(LoadProfile());
        }
        */



        /*
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        User = auth.CurrentUser;
        //StartCoroutine(LoadUserData());
        var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();
        LobbyUIManagerScript.instance.LobbyScreen();
        */
    }

    /*
    private IEnumerator LoadProfile() 
    {

    }
    */

   public void onClickDemo () {
       SceneManager.LoadScene("Game");
   }

    void updatefields()
    {
        Debug.Log("Fetched User Data");
        Debug.Log("Assigning User Data");
        usernameTextField.text = Username;
        GamesPlayedField.text = GamesPlayed;
        GamesWonField.text = GamesWon;
        HighScoreField.text = HighScore;
        Debug.Log("Username for debugging: " + Username);
        Debug.Log("GamesPlayed for debugging: " + GamesPlayed);
        Debug.Log("GamesWon for debugging: " + GamesWon);


        Debug.Log("Loading Lobby Screen");
        LobbyUIManagerScript.instance.LobbyScreen();
    }
    

    private IEnumerator LoadUserData()
    {
        //Get the currently logged in user data
        var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();

        
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            //No data exists yet
            Debug.Log("Initializing user data (first time sign in)");
            FirstTimeLogin = true;

            StartCoroutine(UpdateUsernameDatabase(User.DisplayName));
            Username = User.DisplayName;
            
            StartCoroutine(UpdateGamesPlayed(0));
            GamesPlayed = "0";

            StartCoroutine(UpdateGamesWon(0));
            GamesWon = "0";

            StartCoroutine(UpdateHighScore(0));
            HighScore = "0";
            updatefields();
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            /*
            GamesPlayed = Convert.ToInt32(snapshot.Child("GamesPlayed").Value.ToString());
            GamesWon = Convert.ToInt32(snapshot.Child("GamesWon").Value.ToString());
            HighScore = Convert.ToInt32(snapshot.Child("HighScore").Value.ToString());
            */
            Username = snapshot.Child("username").Value.ToString();
            GamesPlayed = snapshot.Child("GamesPlayed").Value.ToString();
            GamesWon = snapshot.Child("GamesWon").Value.ToString();
            HighScore = snapshot.Child("HighScore").Value.ToString();
            updatefields();
        }
        
    }
    
    private IEnumerator UpdateUsernameDatabase(string _username)
    {
        //Set the currently logged in user username in the database
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("username").SetValueAsync(_username);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
        }
    }

    private IEnumerator UpdateGamesPlayed(int _gamesplayed)
    {
        //Set the currently logged in user GamesPlayed
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("GamesPlayed").SetValueAsync(_gamesplayed);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //GamesPlayed is now updated
        }
    }

    private IEnumerator UpdateGamesWon(int _gameswon)
    {
        //Set the currently logged in user kills
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("GamesWon").SetValueAsync(_gameswon);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //GamesWon are now updated
        }
    }

    private IEnumerator UpdateHighScore(int _highscore)
    {
        //Set the currently logged in user HighScore
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("HighScore").SetValueAsync(_highscore);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //HighScores are now updated
        }
    }
}
