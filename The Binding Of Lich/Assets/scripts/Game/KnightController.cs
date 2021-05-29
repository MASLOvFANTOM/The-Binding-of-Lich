using System;
using System.Collections;
using System.Diagnostics.Contracts;
using UnityEngine;

public class KnightController : ParentCharactrsController
{
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
        if (canAttack && Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        print(Convert.ToInt16(false));
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
                allAttackedObjects[i].gameObject.GetComponent<MainEnemyParametrs>().health -= damage;
            }
        }
    }

    public void ShieldUpDown()
    {
        bool mouseDown = Input.GetMouseButton(1);
        _animator.SetBool("shieldUp", mouseDown);
        realSpeed = lockedSpeed / (Convert.ToInt16(mouseDown) + 1);
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
