using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject torpedoPrefab;
    public Transform firePoint;
    public float attackCooldown = 2f;

    private float nextAttackTime = 0f;
    private bool canAttack = false;

    void Update()
    {
        if (canAttack && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    public void SetCanAttack(bool value)
    {
        canAttack = value;
    }

    private void Attack()
    {
        GameObject torpedo = Instantiate(torpedoPrefab, firePoint.position, firePoint.rotation);
        Torpedo torpedoScript = torpedo.GetComponent<Torpedo>();

        if (torpedoScript != null)
        {
            // Set the target position for the torpedo
            Vector3 playerPosition = GetComponent<EnemyAI>().player.transform.position;
            torpedoScript.SetTargetPosition(playerPosition);
        }
    }
}
