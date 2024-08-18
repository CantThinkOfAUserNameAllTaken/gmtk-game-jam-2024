using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum EnemyType
    {
        Movable,
        Stationary
    }

    public EnemyType enemyType = EnemyType.Movable;
    public float moveSpeed = 5f;
    public bool canEnemyMove => enemyType != EnemyType.Stationary; // Determines if the enemy can move
    public bool chasePlayer = false;

    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform; // Assumes player has "Player" tag
    }

    void Update()
    {
        if (canEnemyMove && chasePlayer)
        {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}
