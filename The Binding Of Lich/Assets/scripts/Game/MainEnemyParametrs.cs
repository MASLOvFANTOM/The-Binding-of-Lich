using System;
using System.Collections;
using UnityEngine;

public class MainEnemyParametrs : MonoBehaviour
{
    public int health = 1; // Жизни
    public bool firstDead; // Для того, что-бы нельзя было умереть ещё раз (показатель смерти)
    public Room myRoom;
    
    

    public virtual IEnumerator Dead()
    {
        yield break;
    }

    

    public virtual void GetDamage(int damage)
    {
        health -= damage;
    }

    private void Update()
    {
        if (health <= 0)
        {
            StartCoroutine(Dead());
        }
    }
}
