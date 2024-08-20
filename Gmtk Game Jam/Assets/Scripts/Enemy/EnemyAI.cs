using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public EnemyState currentState;
    private Transform _player;
    private float _chaseRange;
    private float _attackRange;

    private Enemy _enemy;

    public void Initialize(Enemy enemyReference, float chaseRangeVal, float attackRangeVal)
    {
        _enemy = enemyReference;
        currentState = EnemyState.IDLE;
        _chaseRange = chaseRangeVal;
        _attackRange = attackRangeVal;

        _player = _enemy.GetPlayer();
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
        _enemy.GetEnemyMovement().chasePlayer = false;
    }

    void HandleChaseState()
    {
        // Move towards the player
        if (_enemy.GetEnemyMovement().canEnemyMove)
        {
            _enemy.GetEnemyMovement().chasePlayer = true;
        }
    }

    void HandleAttackState()
    {
        // Attack logic goes here
        _enemy.GetEnemyMovement().chasePlayer = false;
        _enemy.GetEnemyAttack().SetCanAttack(true);
    }

    void StateChange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        if (distanceToPlayer <= _attackRange)
        {
            SetState(EnemyState.ATTACK);
        }
        else if (distanceToPlayer <= _chaseRange && distanceToPlayer> _attackRange)
        {
            SetState(EnemyState.CHASE);
        }
        else
        {
            SetState(EnemyState.IDLE);
        }
    }
}
