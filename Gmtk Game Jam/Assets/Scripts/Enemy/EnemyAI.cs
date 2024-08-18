using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemyState currentState = EnemyState.IDLE;
    public Transform player;
    public float chaseRange = 10f;
    public float attackRange = 2f;
    public float moveSpeed = 5f;

    private Vector3 startingPosition;

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

    void HandleIdleState()
    {
        // Remain still
        transform.position = transform.position;
    }

    void HandleChaseState()
    {
        // Move towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void HandleAttackState()
    {
        // Attack logic goes here, e.g., dealing damage to the player
        Debug.Log("Attacking the player!");
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
