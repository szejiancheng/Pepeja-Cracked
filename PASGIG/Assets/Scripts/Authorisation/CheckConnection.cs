using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;

public class CheckConnection : MonoBehaviour
{
    public void OnCheckConnectionButtonClick () {
        Debug.Log(Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.DisplayName);
    }
}
