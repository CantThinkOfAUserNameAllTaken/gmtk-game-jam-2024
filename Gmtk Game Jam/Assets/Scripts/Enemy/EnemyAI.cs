using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public EnemyState currentState;
    private Transform player;
    private float chaseRange;
    private float attackRange;

    private Enemy enemy;

    public void Initialize(Enemy enemyReference, float chaseRangeVal, float attackRangeVal)
    {
        enemy = enemyReference;
        currentState = EnemyState.IDLE;
        chaseRange = chaseRangeVal;
        attackRange = attackRangeVal;

        player = enemy.GetPlayer();
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
    }

    void HandleIdleState()
    {
        // Remain still
        enemy.GetEnemyMovement().chasePlayer = false;
    }

    void HandleChaseState()
    {
        // Move towards the player
        if (enemy.GetEnemyMovement().canEnemyMove)
        {
            enemy.GetEnemyMovement().chasePlayer = true;
        }
    }

    void HandleAttackState()
    {
        // Attack logic goes here
        enemy.GetEnemyMovement().chasePlayer = false;
        enemy.GetEnemyAttack().SetCanAttack(true);
    }

    void StateChange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            SetState(EnemyState.ATTACK);
        }
        else if (distanceToPlayer <= chaseRange)
        {
            SetState(EnemyState.CHASE);
        }
        else
        {
            SetState(EnemyState.IDLE);
        }
    }
}
