using UnityEngine;

public class PowderDamage : MonoBehaviour
{
    [Range(0, 3)]public int damage;
    [Tooltip("Время горения")]
    [Range(1, 10)]public int burningTime;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCollision"))
        {
            ParentCharactrsController _charactrsController = other.GetComponentInParent<ParentCharactrsController>();
            _charactrsController.GetDamage(damage);
            _charactrsController.StartCoroutine(_charactrsController.FireEffect(burningTime));
        }

        if (other.CompareTag("canAttacked"))
        {
            other.GetComponent<MainEnemyParametrs>().GetDamage(damage);
        }
    }
}
