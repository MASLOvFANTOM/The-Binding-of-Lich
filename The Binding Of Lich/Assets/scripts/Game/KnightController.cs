using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class KnightController : ParentCharactrsController
{
    [Header("For KnightController")]
    private Rigidbody2D rb; // RigidBody
    public float attackRange; // Радиус зоны атаки
    private bool canAttack = true; // Можно ли атаковать
    public float attackCoolDown; // CoolDown атаки
    public bool shieldUp; // Поднят ли щит

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Определение RigidBody
    }

    private void Update()
    {
        Move(rb); // Движение
        ManaAndHealthControl(); // Настройка и показ маны и жизней
        ChangePosPointAttack(); // Поворот точки атаки к машке
        ShieldUpDown(); // Проверка на поднятие щита
        if (canAttack && Input.GetMouseButtonDown(0) && !shieldUp) // Если можно атаковать и щит опущен
        {
            Attack(); // Атака 
        }
    }

    public override void GetDamage(int monsterDamage, bool spawnBlood, bool shieldIgnore)
    {
        if (!invulnerable) // не бессмертный 
        {
            if (shieldUp && stamina > monsterDamage * 1.5 && !shieldIgnore) stamina -= monsterDamage * 1.5f; // Если щит поднят
            else health -= monsterDamage; // Если щит опущен
            
            // if (spawnBlood) SpawnBloodParticle();// Спавн крови

            getDamageAnimation.Play("GetDamage"); // Анимация получения урона(Player)
            camera.GetComponent<CameraController>().CameraShake(); // Тряска камеры
            StartCoroutine(InvulnerableTimer()); // Таймер бессмертия
        }
    }
    private void Attack() // Атака 
    {
        StartCoroutine(AttackCoolDown()); // CoolDown

        _animator.SetTrigger("attack"); // Animation
        
        //Attack
        Collider2D[] allAttackedObjects = Physics2D.OverlapCircleAll(pointAttack.position, attackRange); // Определение области атаки
        for (int i = 0; i < allAttackedObjects.Length; i++)
        {
            if (allAttackedObjects[i].gameObject.CompareTag("canAttacked")) // Если у объекта тэе CanAttacked
            {
                int randCriticalChance = Random.Range(0, 101); // Шанс крита
                int newDamage = Random.Range(damage - 2, damage + 3); // Рандомный крон
                bool critical = false;  

                if (randCriticalChance < criticalChance) // Если выпал крит
                {
                    newDamage = newDamage * 3; // новый крит урон
                    critical = true; 
                }
                
                allAttackedObjects[i].gameObject.GetComponent<MainEnemyParametrs>().GetDamage(newDamage, critical); // Нанесение крона
            }
        }
    }

    public void ShieldUpDown() // Поднятие спуск щита
    {
        bool mouseDown = Input.GetMouseButton(1); // мышь нажата
        _animator.SetBool("shieldUp", mouseDown); // Анимация 
        realSpeed = lockedSpeed / (Convert.ToInt16(mouseDown) + 1); // Уменьшение скорости
        shieldUp = mouseDown; // переменная 
    }

    IEnumerator AttackCoolDown() // Задержка после удара
    {
        canAttack = false; // Запрет Атаки
        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
        StopCoroutine(AttackCoolDown());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(pointAttack.position, attackRange);
    }
}
