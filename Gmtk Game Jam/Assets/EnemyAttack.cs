using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    private float _attackCooldown;
    private float _nextAttackTime;
    private bool _canAttack=false;

    private Enemy _enemy;

    public void Initialize(Enemy enemyReference, float attackCooldownVal, float nextAttackTimeVal)
    {
        _enemy = enemyReference;
        _attackCooldown = attackCooldownVal;
        _nextAttackTime = nextAttackTimeVal;
    }

    void Update()
    {
        if (_canAttack && Time.time >= _nextAttackTime)
        {
            Attack();
            _nextAttackTime = Time.time + _attackCooldown;
        }
    }

    public void SetCanAttack(bool value)
    {
        _canAttack = value;
    }

    private void Attack()
    {
        GameObject torpedo = Instantiate(_enemy.GetTorpedoPrefab(), _enemy.GetTorpedoSpawnPoint().position, Quaternion.identity);
    }
}
