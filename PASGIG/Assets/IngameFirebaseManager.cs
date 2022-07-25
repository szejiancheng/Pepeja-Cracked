using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;

public class IngameFirebaseManager : MonoBehaviour
{
    public FirebaseAuth auth;    
    public FirebaseUser User;
    private DatabaseReference DBreference;

    public string Username;
    public string HighScore;
    public string GamesPlayed;
    public string CumulativeScore;
    private void Start() 
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
        User = auth.CurrentUser;
        StartCoroutine(LoadUserData());
    }

    public void SaveData()
    {
        StartCoroutine(UpdateHighScore(GameMasterScript.GetInstance().CombatScore));
        StartCoroutine(UpdateGamesPlayed());
        StartCoroutine(UpdateCumulativeScore(GameMasterScript.GetInstance().CombatScore));
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
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            Username = snapshot.Child("username").Value.ToString();
            HighScore = snapshot.Child("HighScore").Value.ToString();
            GamesPlayed = snapshot.Child("GamesPlayed").Value.ToString();
            CumulativeScore = snapshot.Child("CumulativeScore").Value.ToString();
        }
        
    }

    private IEnumerator UpdateHighScore(int __combatScore)
    {
        if (Int32.Parse(HighScore) < __combatScore)
        {
            //Set the currently logged in user HighScore
            var DBTask = DBreference.Child("users").Child(User.UserId).Child("HighScore").SetValueAsync(__combatScore);

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

    private IEnumerator UpdateGamesPlayed()
    {
        int __gamesplayed = Int32.Parse(GamesPlayed) + 1;
        //Set the currently logged in user GamesPlayed
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("GamesPlayed").SetValueAsync(__gamesplayed);

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

    private IEnumerator UpdateCumulativeScore(int __combatScore)
    {
        int __cumulativescore = Int32.Parse(CumulativeScore) + __combatScore;

        //Set the currently logged in user kills
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("CumulativeScore").SetValueAsync(__cumulativescore);

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
}
