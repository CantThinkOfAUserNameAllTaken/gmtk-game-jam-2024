using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Torpedo Settings")]
    [SerializeField] private GameObject torpedoPrefab;
    [SerializeField] private Transform torpedoSpawnPoint;

    [Header("AI Settings")]
    [SerializeField] private float chaseRange;
    [SerializeField] private float attackRange;

    private EnemyAI enemyAI;

    [Header("Movement Settings")]
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool chasePlayer = false;
    [SerializeField] private Transform player;

    private EnemyMovement enemyMovement;

    [Header("Attack Settings")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float nextAttackTime;
    [SerializeField] private bool canAttack = false;

    private EnemyAttack enemyAttack;

    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttack = GetComponent<EnemyAttack>();

        InitializeComponents();
    }

    private void InitializeComponents()
    {
        // Initialize AI component
        enemyAI.Initialize(this, chaseRange, attackRange);

        // Initialize Movement component
        enemyMovement.Initialize(this, moveSpeed, rotationSpeed, enemyType, player);

        // Initialize Attack component
        enemyAttack.Initialize(this, attackCooldown, nextAttackTime);
    }

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

    public GameObject GetTorpedoPrefab()
    {
        return torpedoPrefab;
    }

    public Transform GetTorpedoSpawnPoint()
    {
        return torpedoSpawnPoint;
    }

    public Transform GetPlayer()
    {
        return player;
    }

    public EnemyAttack GetEnemyAttack()
    {
        return enemyAttack;
    }

    public EnemyAI GetEnemyAI()
    {
        return enemyAI;
    }

    public EnemyMovement GetEnemyMovement()
    {
        return enemyMovement;
    }
}
