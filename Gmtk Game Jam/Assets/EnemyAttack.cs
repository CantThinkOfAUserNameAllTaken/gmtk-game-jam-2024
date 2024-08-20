using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    private float _attackCooldown;
    private float _nextAttackTime;
    private bool _canAttack=false;

    private Enemy _enemy;

    private AudioList _audioList;

    public void Initialize(Enemy enemyReference, float attackCooldownVal, float nextAttackTimeVal, AudioList audio)
    {
        _enemy = enemyReference;
        _attackCooldown = attackCooldownVal;
        _nextAttackTime = nextAttackTimeVal;
        _audioList = audio;
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
        _audioList.PlaySound("Shooting", gameObject);
        GameObject torpedo = Instantiate(_enemy.GetTorpedoPrefab(), _enemy.GetTorpedoSpawnPoint().position, Quaternion.identity);
    }
}
