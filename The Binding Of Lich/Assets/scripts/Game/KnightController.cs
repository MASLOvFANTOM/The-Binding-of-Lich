using UnityEngine;

public class KnightController : ParentCharactrsController
{
    private Rigidbody2D rb;
    public float attackRange;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move(rb);
        ManaAndHealthControl();
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Attack()
    {
        print("Attack");
        Collider2D[] allAttackedObjects = Physics2D.OverlapCircleAll(pointAttack.position, attackRange);
        for (int i = 0; i < allAttackedObjects.Length; i++)
        {
            print(allAttackedObjects[i].name);
            if (allAttackedObjects[i].gameObject.CompareTag("canAttacked"))
            {
                allAttackedObjects[i].gameObject.GetComponent<MainEnemyParametrs>().health -= damage;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(pointAttack.position, attackRange);
    }
}
