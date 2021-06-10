using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject :  MainEnemyParametrs
{
    [Header("Смерть")]
    public Sprite deadSprite;
    public SpriteRenderer _SpriteRenderer;
    public GameObject deadParticle, breakParticle;
    public Object[] forDead;
    
    [Header("Дроп")] 
    public GameObject[] drop;
    [Tooltip("Оставить НОЛЬ если объект ничего не дропает")]
    [Range(0, 100)] public int rangeChanceDrop;
    
    public override IEnumerator Dead()
    {
        Instantiate(deadParticle, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        
        _SpriteRenderer.sprite = deadSprite;
        SpawnBreakeParticle();
        Drop();
        DestroyAtDead();
        StopCoroutine(Dead());
    }

    private void SpawnBreakeParticle()
    {
        Vector3 breakPArticlePos = new Vector3(transform.position.x, transform.position.y, 1.5f);
        Instantiate(breakParticle, breakPArticlePos, Quaternion.identity);
    }

    public void DestroyAtDead() // Уничтожение ненужных компонентов при смерте(как правило, оставляет только SpriteRenderer)
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
            int randChance = Random.Range(0, 101);
            int randDropItem = Random.Range(0, drop.Length);
            if (randChance >= rangeChanceDrop)
            {
                //Dropping
                Instantiate(drop[randDropItem], transform.position, Quaternion.identity);
            }
        }
    }
}
