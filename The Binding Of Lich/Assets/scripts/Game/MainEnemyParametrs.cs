using System;
using System.Collections;
using UnityEngine;

public class MainEnemyParametrs : MonoBehaviour
{
    [Header("Основа")]
    [Tooltip("Количество жизней")]public int health = 1;
    [Tooltip("Умер объект или нет")]public bool firstDead;
    [Tooltip("Только для врагов")]public Room myRoom;

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
        if (health <= 0 && !firstDead)
        {
            firstDead = true;
            StartCoroutine(Dead());
        }
    }
}
