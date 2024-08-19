using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public float moveSpeed = 2f;          // Speed at which the mine moves
    private Vector2 targetPosition;       // Target position within the MineArea
    private MineArea mineArea;

    private void Start()
    {
        mineArea = GetComponentInParent<MineArea>();  // Access the MineArea script from the parent
        StartCoroutine(MoveToRandomPosition());
    }

    private IEnumerator MoveToRandomPosition()
    {
        while (true)
        {
            // Get a new random target position within the MineArea
            targetPosition = mineArea.GetRandomPointInArea();

            while ((Vector2)transform.position != targetPosition)
            {
                // Move the mine towards the target position
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            // Wait for a brief moment before moving again
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<EnemyHealth>() != null || other.GetComponent<PlayerHealth>() != null || other.GetComponent<Torpedo>()!=null)
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if(enemy != null)
            {
                
            }
            if(player != null)
            {

            }

            
        }
        Explode();
    }

    private void Explode()
    {
 
        // Add explosion effect or sound here
        Destroy(gameObject);
    }
}
