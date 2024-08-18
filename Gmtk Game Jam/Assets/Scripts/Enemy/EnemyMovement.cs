using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum EnemyType
    {
        Movable,
        Stationary
    }

    public EnemyType enemyType = EnemyType.Movable;
    public float moveSpeed = 5f;
    public float rotationSpeed = 2f; 
    public bool canEnemyMove => enemyType != EnemyType.Stationary; // Determines if the enemy can move
    public bool chasePlayer = false;

    public Transform front;
    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform; // Assumes player has "Player" tag
    }

    void Update()
    {
        if (canEnemyMove && chasePlayer)
        {
            ChasePlayer();
            RotateTowardsPlayer();
        }
        else if(canEnemyMove && !chasePlayer)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
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
        direction.z = 0; // Ensure rotation only happens in 2D

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        if (direction.x > 0) // Player is on the right side
        {
            angle = Mathf.Clamp(angle, -5f, 5f);
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle); // Face right
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else // Player is on the left side
        {
            angle = Mathf.Clamp(angle, -5f, 5f);
            Quaternion targetRotation = Quaternion.Euler(0, 0, -angle); // Face left with a flip
            gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // If the player is directly above or below the enemy, keep the z-axis rotation at 0
        if (Mathf.Abs(direction.x) == 0)
        {
            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);//s front.rotation.eulerAngles.y, 0);

            gameObject.transform.rotation = targetRotation;//gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

    }
}
