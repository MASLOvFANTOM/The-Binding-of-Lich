using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    private Transform playerPosition;
    private Vector2 target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, target) > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        else ChengeTargetPosition();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("obstacle")) ChengeTargetPosition();
    }

    public void ChengeTargetPosition()
    {
        int randChance = Random.Range(0, 101);
        if (randChance < 50) target = new Vector2(target.x + Random.Range(-2, 2), target.y + Random.Range(-2, 2));
        else target = playerPosition.position;
    }
}
