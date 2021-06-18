using System;
using System.Collections;
using UnityEngine;

public class HeavyEnemyController : MainEnemyParametrs
{
    public ParticleSystem bloodParticle, damageWave;
    public EnemyFollow _enemyFollow;
    public Animator _animator;
    public Transform pointAttack;
    public GameObject player;
    public GameObject[] allForFlip;
    public float coolDown, thisCoolDown;
    private bool canAttack = true;
        
    private void Start()
    {
        // определение всех объектов
        _enemyFollow = GetComponent<EnemyFollow>();
        _animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void FixedUpdate()
    {
        // Определение угла поворота к персонажу
        float angle = (transform.position.x - player.transform.position.x);
        angle = Mathf.Clamp(angle, 0, 1);
        angle = angle * 180;
        
        Flip(angle);
    }
    
    public void Flip(float angle) // Разворот всех спратов под нужный угол по оси Y
    {
        for (int i = 0; i < allForFlip.Length; i++)
        {
            allForFlip[i].transform.rotation = new Quaternion(0, angle, 0, 0);
        }
    }

    public override void GetDamage(int damage, bool critical)
    {
        base.GetDamage(damage, critical);
        StartCoroutine(BloodParticleSpart()); // Создание частиц крови
    }
    
    public IEnumerator BloodParticleSpart() // Создание частиц крови
    {
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, 1.5f); // правилльная позиция
        Instantiate(bloodParticle, newPos, Quaternion.Euler(-90, 0 ,0)); // создание
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator Attack() // Атака
    {
        if (canAttack)
        {
            thisCoolDown = 0;
            _animator.SetTrigger("Attack"); // аниматор

            yield return new WaitForSeconds(0.6f);
            Instantiate(damageWave, pointAttack.position, Quaternion.identity); // Создать ударную воллну
        
            float speed = _enemyFollow.speed; // Сохранить скорость
            _enemyFollow.speed = 0; // Остановить монстра
            yield return new WaitForSeconds(coolDown);
        
            _enemyFollow.speed = speed; // вернуть скорость
            thisCoolDown = coolDown + 1;
        }
    }

    public override IEnumerator Dead() // Смерть
    {
        canAttack = false;
        _animator.SetTrigger("dead"); // Аниматор
        myRoom.monstersInRoom.Remove(gameObject); // Удаление объекта их списка комнаты
        yield return new WaitForSeconds(1f); 
        Destroy(gameObject); // Уничтожение
    }

    private void OnTriggerStay2D(Collider2D other) // Триггер на атаку
    {
        if (thisCoolDown >= coolDown) 
        {
            if (other.CompareTag("PlayerCollision")) // Удар по игроку 
            {
                StartCoroutine(Attack());
            }
            else if (other.CompareTag("canAttacked")) // Удар по коробкам
            {
                StartCoroutine(Attack());
            }
        }
    }
}
