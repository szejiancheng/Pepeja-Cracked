using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeCollision : MonoBehaviour
{
    public EnemyHP enemyHP;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.CompareTag("Player"))
        {
            SelfDestruct();
            PlayerHP playerHP = collision.gameObject.GetComponent("PlayerHP") as PlayerHP;
            playerHP.TakeDamage(10);
        }

        if(collision.gameObject.CompareTag("Missile"))
        {
            DoDamageToEnemy(100);
        }

        if(collision.gameObject.CompareTag("Bullet"))
        {
            DoDamageToEnemy(50);
        }
    }

    void SelfDestruct()
    {
        Debug.Log("Kamikaze Destroyed!");
        Destroy(gameObject);
    }

    void DoDamageToEnemy(int damagetaken)
    {
        if(enemyHP!=null)
        {
        Debug.Log("Enemy took damage!");
        enemyHP.TakeDamage(damagetaken);
        }
    }
}



