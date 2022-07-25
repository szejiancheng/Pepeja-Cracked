using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HPBar healthBar;
    public GameOverScript gameover;
    public PlayerScore playerscore;

    public PlayerMain playerMain;
    public GameObject DestPrefab;

    void Awake()
    {
        playerMain = gameObject.GetComponent<PlayerMain>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update() 
    {
        /***
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            TakeDamage(10);
        } 
        ***/    
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Took damage: " + damage);

        healthBar.SetHealth(currentHealth);

         if(currentHealth < 0)
         {
             playerMain.DestroyPlayer();
         }
    }
}
