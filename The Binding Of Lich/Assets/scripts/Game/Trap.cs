using System;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [Range(0, 10)]public int damage; // Урон
    [Range(0, 20)] public int time; // Время действия эффекта
    public string trapEffect; // Имя эффета
    public bool activated; // Актиаировано-ли
    public SpriteRenderer _SpriteRenderer; // Спрайтрендерер
    public Sprite activitedSprite; // Активированное состояние

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!activated)
        {
            if (other.CompareTag("Player")) // Если игрок
            {
                other.GetComponent<ParentCharactrsController>().GetDamage(damage, true, true);
                ParentCharactrsController charactrsController = other.GetComponent<ParentCharactrsController>();
                activated = true;
                _SpriteRenderer.sprite = activitedSprite;
                
                switch (trapEffect) // Наложение эффекта
                {
                    case "simple":
                        charactrsController.StartCoroutine(charactrsController.BleedingEffect(time));
                        break;
                    case "poison":
                        charactrsController.StartCoroutine(charactrsController.PoisonEffect(time));
                        break;
                    case "blindness":
                        charactrsController.StartCoroutine(charactrsController.BlindnessEffect(time));
                        break;
                    case "freeze":
                        charactrsController.StartCoroutine(charactrsController.FreezeEffect(time));
                        break;
                }
            }
        }
    }
}
