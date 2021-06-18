using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using Object = UnityEngine.Object;

public class SwordsmanController : MainEnemyParametrs
{
    public ParticleSystem bloodParticle;
    public EnemyFollow _enemyFollow;
    public AttackZone _attackZone;
    public Animator _animator;

    private void Start()
    {
        _enemyFollow = GetComponent<EnemyFollow>();
        _attackZone = GetComponentInChildren<AttackZone>();
        _animator = GetComponent<Animator>();
    }

    public override IEnumerator Dead()
    {
        _animator.SetTrigger("dead");
        myRoom.monstersInRoom.Remove(this.gameObject);
        firstDead = true;
        _enemyFollow.speed = 0;
        _attackZone.canAttack = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public override void GetDamage(int damage, bool critical)
    {
        base.GetDamage(damage, critical);
        StartCoroutine(BloodParticleStart());
    }
    
    public IEnumerator BloodParticleStart()
    {
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, 1.4f);
        Instantiate(bloodParticle, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
    }
}
