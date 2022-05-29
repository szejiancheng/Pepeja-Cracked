using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ConnectToPhotonManager : MonoBehaviourPunCallbacks
{   

    
    public TMP_Text buttonText;
    public LoadManager loadmanager;
    public string username; 

    public void OnClickConnect() {
        Debug.Log("Clicked connect button");
        username = loadmanager.instance.Username;
        if (username.Length >= 1) {
            PhotonNetwork.NickName = username;
            buttonText.text = "Connecting...";
            PhotonNetwork.ConnectUsingSettings();
        }

        
    }
    public override void OnConnectedToMaster() {
        SceneManager.LoadScene("Multiplayer Lobby");
    }
}
