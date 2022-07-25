using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject DestEffect;

    void Awake()
    {

    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            EnemyDestroyed();
        }
    }

    void EnemyDestroyed()
    {
        Instantiate(DestEffect, transform.position, transform.rotation);
        GameMasterScript.GetInstance().AddScore(maxHealth);
        //playerScore.AddScore(maxHealth);
        Destroy(gameObject);
    }
}
