using System.Collections;
using UnityEngine;

public class SwordsmanController : MainEnemyParametrs
{
    private PolygonCollider2D damageCollider;
    public int damage = 1;
    public float coolDown = 1f;
    public bool canAttack = true;


    private void Start()
    {
        damageCollider = GetComponent<PolygonCollider2D>();
    }

    private void FixedUpdate()
    {
        if (firstDead) // Dead
        {
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Нанесение урона
        if (!firstDead)
        {
            if (other.gameObject.CompareTag("PlayerCollision"))
            {
                if (canAttack)
                {
                    other.transform.parent.gameObject.GetComponent<ParentCharactrsController>().health -= damage;
                    canAttack = false;
                    StartCoroutine(CoolDownTimer());
                }
            }
        }
    }

    IEnumerator CoolDownTimer()
    {
        yield return new WaitForSeconds(coolDown);
        canAttack = true;
        StopCoroutine(CoolDownTimer());
    }
}
