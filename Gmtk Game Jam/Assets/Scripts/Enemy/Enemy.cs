using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private EnemyMovement enemyMovement;

    [SerializeField] public GameObject torpedoPrefab;
    public Transform torpedoSpawnPoint;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the enemy collided with a wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Set the enemy to the idle state
            enemyAI.SetState(EnemyState.IDLE);
            // Stop movement
            enemyMovement.chasePlayer = false;
        }
    }
}
