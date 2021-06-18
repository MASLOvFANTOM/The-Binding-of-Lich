using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MainEnemyParametrs : MonoBehaviour
{
    [Header("Основа")]
    [Tooltip("Количество жизней")]public int health; // Жизни
    [Tooltip("Умер объект или нет")]public bool firstDead; // Смерть
    [Tooltip("Только для врагов")]public Room myRoom; // Моя комнота
    public GameAssets _GameAssets;

    public virtual IEnumerator Dead()
    {
        yield break;
    }

    private void Awake()
    {
        _GameAssets = GameObject.FindGameObjectWithTag("GameAssets").GetComponent<GameAssets>();
        health = Random.Range(health - 2, health + 3); // Рандомизация жизней
    }

    public virtual void GetDamage(int damage, bool critical) // Получение крона
    {
        _GameAssets._DamagePopUp.create(transform.position, damage, critical); 
        
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
