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

    public override void GetDamage(int damage)
    {
        base.GetDamage(damage);
        StartCoroutine(BloodParticleStart());
    }
    
    public IEnumerator BloodParticleStart()
    {
        bloodParticle.Play();
        yield return new WaitForSeconds(1f);
        bloodParticle.Stop();
    }
}
