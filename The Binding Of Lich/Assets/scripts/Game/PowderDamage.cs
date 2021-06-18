using UnityEngine;

public class PowderDamage : MonoBehaviour
{
    [Range(0, 40)]public int damage; // Урон
    [Tooltip("Время горения")]
    [Range(1, 10)]public int burningTime; // Время горения
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerCollision")) // Если игрок
        {
            ParentCharactrsController _charactrsController = other.GetComponentInParent<ParentCharactrsController>();
            _charactrsController.GetDamage(damage, false, false);
            _charactrsController.StartCoroutine(_charactrsController.FireEffect(burningTime));
        }

        if (other.CompareTag("canAttacked")) // Если бочка
        {
            other.GetComponent<MainEnemyParametrs>().GetDamage(damage, false);
        }
    }
}
