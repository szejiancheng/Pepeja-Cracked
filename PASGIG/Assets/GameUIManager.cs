using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField]
    GameObject GameUI;
    [SerializeField]
    GameObject PauseMenu;
    
    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        GameMasterScript.GetInstance().Pause();
        GameUI.SetActive(false);        
    }
    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        GameMasterScript.GetInstance().Unpause();
        GameUI.SetActive(true);        
    }


}
