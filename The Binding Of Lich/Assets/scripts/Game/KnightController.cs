using System.Collections;
using System.Diagnostics.Contracts;
using UnityEngine;

public class KnightController : ParentCharactrsController
{
    private Rigidbody2D rb;
    public float attackRange;
    private bool canAttack = true;
    public float attackCoolDown;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move(rb);
        ManaAndHealthControl();
        if (canAttack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }
        }
    }
    

    private void Attack()
    {
        StartCoroutine(AttackCoolDown()); // CoolDown
        // rotate to mouse
        if (pointAttack.parent.transform.rotation.z < 0.99 && pointAttack.parent.transform.rotation.z > 0)
            _SpriteRenderer.flipX = false;
        else _SpriteRenderer.flipX = true;
        
        // Animation & attack processing;
        _animator.SetTrigger("Attack");
        Collider2D[] allAttackedObjects = Physics2D.OverlapCircleAll(pointAttack.position, attackRange);
        for (int i = 0; i < allAttackedObjects.Length; i++)
        {
            if (allAttackedObjects[i].gameObject.CompareTag("canAttacked"))
            {
                allAttackedObjects[i].gameObject.GetComponent<MainEnemyParametrs>().health -= damage;
            }
        }
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
