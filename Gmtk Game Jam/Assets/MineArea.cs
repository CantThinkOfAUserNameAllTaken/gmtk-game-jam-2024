using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineArea : MonoBehaviour
{
    public float radius = 5f;  // Adjustable radius in the inspector

    private void OnDrawGizmos()
    {
        // Draw the MineArea in the editor for visualization
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    // Function to get a random point within the MineArea
    public Vector2 GetRandomPointInArea()
    {
        return (Vector2)transform.position + Random.insideUnitCircle * radius;
    }
}
