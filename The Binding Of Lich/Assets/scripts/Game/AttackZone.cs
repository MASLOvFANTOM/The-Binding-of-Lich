using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    public bool canAttack = true, attackInteractiveOBJ;
    public int damage = 1;
    public float coolDown = 1f;

    private void OnTriggerStay2D(Collider2D other)
    {
        // Нанесение урона
        if (canAttack)
        {
            if (other.gameObject.CompareTag("PlayerCollision")) // Удар по игроку
            {
                ParentCharactrsController characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<ParentCharactrsController>();
                characterController.GetDamage(damage, false, false);
                StartCoroutine(AttackCoolDown());
            }
            
            if (other.CompareTag("canAttacked") && other.gameObject.layer == 7 && attackInteractiveOBJ) // Удар по интерактивны объектам
            {
                MainEnemyParametrs enemyParametrs = other.GetComponent<MainEnemyParametrs>();
                enemyParametrs.GetDamage(damage * 7, false);
                StartCoroutine(AttackCoolDown());
            }
        }

        
    }
    IEnumerator AttackCoolDown() // Задержка после удара
    {
        canAttack = false;
        yield return new WaitForSeconds(coolDown);
        canAttack = true;
        StopCoroutine(AttackCoolDown());
    }
}
