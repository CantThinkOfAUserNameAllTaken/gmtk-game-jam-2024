using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    [SerializeField]
    private GameObjectList player;

    private Transform _target;

    private NavMeshAgent _agent;

    public bool chasePlayer = false;

    public bool canEnemyMove = true;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateUpAxis = false;
        _agent.updateRotation = false;
        _target = player.GetItemAtIndex(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!chasePlayer && !canEnemyMove)
        {
            _agent.SetDestination(transform.position);
        }
        _agent.SetDestination(_target.position);
    }
}
