using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class InteractiveObject :  MainEnemyParametrs
{
    [Header("Смерть")]
    public Sprite deadSprite;
    public SpriteRenderer _SpriteRenderer;
    public GameObject dustParticle, breakParticle;
    public Object[] forDead;
    
    [Header("Дроп")] 
    
    [Tooltip("Объекты дропа")]
    public GameObject[] drop;
    
    [Tooltip("Оставить НОЛЬ если объект ничего не дропает")]
    [Range(0, 100)] public int rangeChanceDrop;

    [Tooltip("Количество дропа")] public int dropValue = 1;

    public override IEnumerator Dead() // Смерть
    {
        SpawnDustParticle();
        yield return new WaitForSeconds(0.1f);
        
        SpawnBreakeParticle();
        Drop();
        DestroyAtDead();
        Destroy(gameObject);
        StopCoroutine(Dead());
    }

    private void SpawnBreakeParticle() // Создание партиклов остатков
    {
        Vector3 breakPArticlePos = new Vector3(transform.position.x, transform.position.y, 1.5f);
        Instantiate(breakParticle, breakPArticlePos, Quaternion.Euler(-90, 0 ,0));
    }

    private void SpawnDustParticle() // Создание пыли при разрушении
    {
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, 1.5f);
        Instantiate(dustParticle, newPos, Quaternion.identity);
    }

    public void DestroyAtDead() // Уничтожение ненужных компонентов при смерте
    {
        for (int i = 0; i < forDead.Length; i++)
        {
            Destroy(forDead[i]);
        }
    }

    public void Drop() // Дроп
    {
        if (drop.Length != 0)
        {
            for (int i = 0; i < dropValue; i++)
            {
                int randChance = Random.Range(0, 101); // Опредеение шанса
                int randDropItem = Random.Range(0, drop.Length); // Определение предмета
                if (randChance <= rangeChanceDrop)
                {
                    //Dropping
                    Instantiate(drop[randDropItem], transform.position, Quaternion.identity);
                }
            }
        }
    }
}
