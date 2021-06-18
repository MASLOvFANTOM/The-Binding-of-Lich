using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject :  MainEnemyParametrs
{
    [Header("Смерть")]
    public Sprite deadSprite;
    public SpriteRenderer _SpriteRenderer;
    public GameObject dustParticle, breakParticle;
    public Object[] forDead;
    
    [Header("Дроп")] 
    public GameObject[] drop;
    [Tooltip("Оставить НОЛЬ если объект ничего не дропает")]
    [Range(0, 100)] public int rangeChanceDrop;
    
    public override IEnumerator Dead() // Смерть
    {
        SpawnDustParticle(); // создать партикллы разрушения(дым)
        yield return new WaitForSeconds(0.1f);
        
        SpawnBreakeParticle(); // создать партиклы осколлков
        Drop(); // Выдача дропа
        DestroyAtDead(); // Уничтожить ненужные компоненты
        StopCoroutine(Dead());
    }

    private void SpawnBreakeParticle()
    {
        Vector3 breakPArticlePos = new Vector3(transform.position.x, transform.position.y, 1.5f); // правильная позиция
        Instantiate(breakParticle, breakPArticlePos, Quaternion.Euler(-90, 0 ,0)); // создание партиклов
    }

    private void SpawnDustParticle()
    {
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, 1.5f); // правильная позиция
        Instantiate(dustParticle, newPos, Quaternion.identity); // создание партиклов
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
            int randChance = Random.Range(0, 101); // Опредеение шанса
            int randDropItem = Random.Range(0, drop.Length); // Определение предмета
            if (randChance >= rangeChanceDrop)
            {
                //Dropping
                Instantiate(drop[randDropItem], transform.position, Quaternion.identity);
            }
        }
    }
}
