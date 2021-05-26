using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MainEnemyParametrs : MonoBehaviour
{
    public int health;
    public GameObject drop;
    public int[] chanceDrop = new int[2];
    private Animator _animator;
    private bool firstDead;
    public string deadTag;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Dead
        if (health <= 0 && !firstDead)
        {
            firstDead = true;
            _animator.SetTrigger(deadTag);
            Destroy(GetComponent<BoxCollider2D>());
            
            // Dropping
            if (drop)
            {
                int random = Random.Range(0, 101);
                if (random >= chanceDrop[0] && random <= chanceDrop[1])
                {
                    Instantiate(drop, transform.position, Quaternion.identity);
                }
            }
        }
    }
}
