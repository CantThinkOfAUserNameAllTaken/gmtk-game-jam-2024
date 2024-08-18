using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private Enemy enemy;
    private float moveSpeed;
    private float rotationSpeed;
    public bool canEnemyMove;// => enemyType != EnemyType.Stationary;
    public bool chasePlayer = false;

    private Transform player;
    private Transform front;

    public void Initialize(Enemy enemyReference, float moveSpeedVal, float rotationSpeedVal, EnemyType enemyType, Transform playerRef)
    {
        enemy = enemyReference;

        player = playerRef;
        moveSpeed = moveSpeedVal;
        rotationSpeed = rotationSpeedVal;
        canEnemyMove = enemyType != EnemyType.Stationary;
    }

    void Update()
    {
        if (canEnemyMove && chasePlayer)
        {
            ChasePlayer();
            RotateTowardsPlayer();
        }
    }

    void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void RotateTowardsPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.z = 0;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (direction.x > 0)
        {
            angle = Mathf.Clamp(angle, -5f, 5f);
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            angle = Mathf.Clamp(angle, -5f, 5f);
            Quaternion targetRotation = Quaternion.Euler(0, 0, -angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (Mathf.Abs(direction.x) == 0)
        {
            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = targetRotation;
        }
    }
}
public enum EnemyType
{
    Movable,
    Stationary
}