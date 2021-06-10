using System;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [Range(0, 10)]public int damage;
    [Range(0, 20)] public int time;
    public string trapEffect;
    public bool activated;
    public SpriteRenderer _SpriteRenderer;
    public Sprite activitedSprite;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!activated)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<ParentCharactrsController>().GetDamage(damage, true);
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
