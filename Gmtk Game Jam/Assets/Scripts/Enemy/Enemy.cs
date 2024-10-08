using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Torpedo Settings")]
    [SerializeField] private GameObject _torpedoPrefab;
    [SerializeField] private Transform _torpedoSpawnPoint;

    [Header("AI Settings")]
    [SerializeField] private float _chaseRange;
    [SerializeField] private float _attackRange;

    private EnemyAI _enemyAI;

    [Header("Movement Settings")]
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private bool _chasePlayer = false;
    [SerializeField]
    private GameObjectList _playerList;

    private Transform _player;

    private EnemyNavMesh _enemyMovement;

    [Header("Attack Settings")]
    [SerializeField] private float _attackCooldown;
    [SerializeField] private float _nextAttackTime;
    [SerializeField] private bool _canAttack = false;

    private EnemyAttack _enemyAttack;

    [SerializeField]
    private AudioList _attackAudioList;

    private void Start()
    {
        _player = _playerList.GetItemAtIndex(0).transform;
    }
    private void Awake()
    {
        _enemyAI = GetComponent<EnemyAI>();
        _enemyMovement = GetComponent<EnemyNavMesh>();
        _enemyAttack = GetComponent<EnemyAttack>();

        InitializeComponents();
    }

    private void InitializeComponents()
    {
        // Initialize AI component
        _enemyAI.Initialize(this, _chaseRange, _attackRange);

        // Initialize Attack component
        _enemyAttack.Initialize(this, _attackCooldown, _nextAttackTime, _attackAudioList);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the enemy collided with a wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Set the enemy to the idle state
            _enemyAI.SetState(EnemyState.IDLE);
            // Stop movement
            _enemyMovement.chasePlayer = false;
        }
    }

    public GameObject GetTorpedoPrefab()
    {
        return _torpedoPrefab;
    }

    public Transform GetTorpedoSpawnPoint()
    {
        return _torpedoSpawnPoint;
    }

    public Transform GetPlayer()
    {
        return _player;
    }

    public EnemyAttack GetEnemyAttack()
    {
        return _enemyAttack;
    }

    public EnemyAI GetEnemyAI()
    {
        return _enemyAI;
    }

    public EnemyNavMesh GetEnemyMovement()
    {
        return _enemyMovement;
    }
}
