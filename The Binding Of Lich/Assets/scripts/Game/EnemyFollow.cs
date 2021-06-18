using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyFollow : MonoBehaviour
{
    public float speed; // Скорость движения
    public float stoppingDistance; // Минимальная дистанция до цели
    public float coolDownForTargetChange, thisCoolDownForTargetChange, obstacleCoolDownForTargetChange;
    private Transform playerPosition; // Игрок
    private Vector2 target; // Цель

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, target) > stoppingDistance) // Если цель далеко
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime); // движение к цели
        }
        else ChangeTargetPosition(); // Если цель близко; //
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer == 7) // Если каснулся OBSTACLE
        {
            obstacleCoolDownForTargetChange += Time.fixedDeltaTime;
            if (thisCoolDownForTargetChange >= coolDownForTargetChange) // Если проверка возможна
            {
                StartCoroutine(TimeCoolDownObstacle()); // Запустить таймер
                ExtraChangeTargetPosition(); //Большое изменение позицыи цели
            }

            if (obstacleCoolDownForTargetChange >= coolDownForTargetChange)
            {
                ExtraChangeTargetPosition();
                obstacleCoolDownForTargetChange = 0f;
            }
        }
    }

    public void ChangeTargetPosition() // Немного изменить позицию цели (для рандомного эффекта)
    {
        int randChance = Random.Range(0, 101); // шанс
        if (randChance < 50) target = new Vector2(target.x + Random.Range(-2, 3), target.y + Random.Range(-2, 3)); // Двигаю в сторону
        else StartCoroutine(ChangePlayerTarget()); // Двигаю на игрока
    }
    public void ExtraChangeTargetPosition() // Большое изменение позицыи цели
    {
        // int randChance = Random.Range(0, 101); // шанс
        // if (randChance < 75)
        target = new Vector2(target.x + Random.Range(-5, 6), target.y + Random.Range(-5, 6)); // Двигаю в сторону
        // else StartCoroutine(ChangePlayerTarget()); // Двигаю за игроком
    }

    IEnumerator ChangePlayerTarget() // Двигаю на игрока
    {
        for (int i = 0; i < 4; i++)
        {
            target = playerPosition.position;
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator TimeCoolDownObstacle() // Таймер на стоковения
    {
        /// Принцив таков. Это для того, что бы объект(враг) мог отойти от стены и толлько потом проверять соприкосновение с ней.
        
        thisCoolDownForTargetChange = 0;
        yield return new WaitForSeconds(coolDownForTargetChange);
        thisCoolDownForTargetChange = coolDownForTargetChange + 1;
    }
}
