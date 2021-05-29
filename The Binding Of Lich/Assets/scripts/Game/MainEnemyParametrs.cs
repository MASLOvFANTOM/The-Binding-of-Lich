using UnityEngine;
using Random = UnityEngine.Random;

public class MainEnemyParametrs : MonoBehaviour
{
    public int health; // Жизни
    public int[] chanceDrop = new int[2]; // От x - до y % Шанс дропа предмета
    public bool firstDead; // Для того, что-бы нельзя было умереть ещё раз (показатель смерти)
    public string deadAnimationTriggerTag; // Имя триггера анимации смерти
    public GameObject drop; // Предмет дропа.
    private Animator _animator; // Аниматор
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Dead
        if (health <= 0 && !firstDead)
        {
            Dead();
            DropItem(drop);
        }
    }

    public void Dead() // Объект уничтожен
    {
        firstDead = true;
        _animator.SetTrigger(deadAnimationTriggerTag);
        if(GetComponent<BoxCollider2D>()) Destroy(GetComponent<BoxCollider2D>()); //Optimization
    }

    public void DropItem(GameObject item) // Выбросить указанный объект
    {
        int random = Random.Range(0, 101);
        if (random >= chanceDrop[0] && random <= chanceDrop[1])
        { 
            Instantiate(item, transform.position, Quaternion.identity);
        }
    }
}
