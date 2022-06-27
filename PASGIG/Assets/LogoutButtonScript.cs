using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoutButtonScript : MonoBehaviour
{
    public void WhenClicked()
    {
        AuthManager.instance.LogOutButton();
    }
}
