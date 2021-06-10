using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    public bool canAttack = true;
    public int damage = 1;
    public float coolDown = 1f;
    public Transform lookAtPlayer;
    public Transform player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        lookAtPlayer.LookAt(player);
        lookAtPlayer.rotation = Quaternion.Euler(0,0, (lookAtPlayer.rotation.y * lookAtPlayer.rotation.x) * 100);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Нанесение урона
        if (other.gameObject.CompareTag("PlayerCollision"))
        {
            if (canAttack)
            {
                ParentCharactrsController characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<ParentCharactrsController>();
                characterController.GetDamage(damage, true);
                canAttack = false;
                StartCoroutine(AttackCoolDown());
            }
        }
    }
    IEnumerator AttackCoolDown() // Отчет времени без атаки
    {
        yield return new WaitForSeconds(coolDown);
        canAttack = true;
        StopCoroutine(AttackCoolDown());
    }
}
