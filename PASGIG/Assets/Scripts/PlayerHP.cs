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

    public PlayerMain playerscript;

    void Awake()
    {
        playerscript = gameObject.GetComponent<PlayerMain>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            TakeDamage(10);
        }     
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

         if(currentHealth < 0)
         {
             PlayerDestroyed();
         }
    }

    void PlayerDestroyed()
    {
        //Destroy(gameObject);
        GameOver();

    }

    void GameOver()
    {
        //gameover.InitScore(playerscore.score);
    }
}
