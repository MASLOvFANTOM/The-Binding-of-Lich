using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class KnightController : ParentCharactrsController
{
    [Header("For KnightController")]
    private Rigidbody2D rb;
    public float attackRange;
    private bool canAttack = true;
    public float attackCoolDown;
    public bool shieldUp;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move(rb);
        ManaAndHealthControl();
        ChangePosPointAttack();
        ShieldUpDown();
        if (canAttack && Input.GetMouseButtonDown(0) && !shieldUp)
        {
            Attack();
        }
    }

    public override void GetDamage(int monsterDamage, bool spawnBlood)
    {
        if (!invulnerable)
        {
            if (shieldUp && stamina > monsterDamage * 20) stamina -= monsterDamage * 20;
            else health -= monsterDamage;
            if (spawnBlood)
            {
                Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y, 1.4f);
                Instantiate(particleBlood, spawnPos, Quaternion.identity);
            }
            getDamageAnimation.Play("GetDamage");
            camera.GetComponent<CameraController>().CameraShake();
            StartCoroutine(InvulnerableTimer());
        }
    }

    public void CameraGetDamage(int range)
    {
        for (int i = 0; i < range; i++)
        {
            float rand = Random.Range(-0.5f, 0.5f);
            Vector3 newPos = new Vector3(camera.transform.position.x + rand, camera.transform.position.y + rand, -10);
            camera.transform.DOMove(newPos, 0.01f);
        }
    }
    private void Attack()
    {
        StartCoroutine(AttackCoolDown()); // CoolDown

        // Animation & attack processing;
        _animator.SetTrigger("attack");
        Collider2D[] allAttackedObjects = Physics2D.OverlapCircleAll(pointAttack.position, attackRange);
        for (int i = 0; i < allAttackedObjects.Length; i++)
        {
            if (allAttackedObjects[i].gameObject.CompareTag("canAttacked"))
            {
                allAttackedObjects[i].gameObject.GetComponent<MainEnemyParametrs>().GetDamage(damage);
            }
        }
    }

    public void ShieldUpDown()
    {
        bool mouseDown = Input.GetMouseButton(1);
        _animator.SetBool("shieldUp", mouseDown);
        realSpeed = lockedSpeed / (Convert.ToInt16(mouseDown) + 1);
        shieldUp = mouseDown;
    }

    IEnumerator AttackCoolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCoolDown);
        canAttack = true;
        StopCoroutine(AttackCoolDown());
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(pointAttack.position, attackRange);
    }
}
