using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemyState currentState = EnemyState.IDLE;
    public Transform player;
    public float chaseRange = 10f;
    public float attackRange = 2f;

    private EnemyMovement enemyMovement;
    private Enemy enemy;
    private EnemyAttack enemyAttack; 
    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemy = GetComponent<Enemy>();
        enemyAttack = GetComponent<EnemyAttack>();
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.IDLE:
                HandleIdleState();
                break;
            case EnemyState.CHASE:
                HandleChaseState();
                break;
            case EnemyState.ATTACK:
                HandleAttackState();
                break;
        }

        StateChange();
    }

    public void SetState(EnemyState newState)
    {
        if (newState != currentState)
        {
            currentState = newState;
        }
        
        // Additional logic can be added here if needed, e.g., resetting animations
    }
    void HandleIdleState()
    {
        // Remain still
        enemyMovement.chasePlayer = false;
    }

    void HandleChaseState()
    {
        // Move towards the player
        if (enemyMovement.canEnemyMove)
        {
            enemyMovement.chasePlayer = true;
        }
    }

    void HandleAttackState()
    {
        // Attack logic goes here, e.g., dealing damage to the player
        enemyMovement.chasePlayer = false;
        enemyAttack.SetCanAttack(true);
    }

    void ShootTorpedo()
    {
        GameObject torpedo = Instantiate(enemy.torpedoPrefab, enemy.torpedoSpawnPoint.position, Quaternion.identity);
        Torpedo torpedoScript = torpedo.GetComponent<Torpedo>();
        torpedoScript.SetTargetPosition(player.position);
    }

    void StateChange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            currentState = EnemyState.ATTACK;

        }
        else if (distanceToPlayer <= chaseRange)
        {
            currentState = EnemyState.CHASE;
        }
        else
        {
            currentState = EnemyState.IDLE;
        }
    }
}
