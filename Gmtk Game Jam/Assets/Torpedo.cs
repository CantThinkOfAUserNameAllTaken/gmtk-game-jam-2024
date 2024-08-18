using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : MonoBehaviour
{
    public enum TorpedoState
    {
        Launching,
        Fired,
        Contact
    }

    public TorpedoState currentState = TorpedoState.Launching;

    private Vector3 targetPosition;
    public float initialMoveTime = 0.5f;
    public float initialMoveSpeed = 0.5f;
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(HandleLaunching());
    }
    public void SetTargetPosition(Vector3 targetPos)
    {
        targetPosition = targetPos;
    }

    private IEnumerator HandleLaunching()
    {
        // While in the Launching state, move up slowly
        Vector3 initialPosition = transform.position;
        float elapsedTime = 0;

        while (elapsedTime < initialMoveTime && currentState == TorpedoState.Launching)
        {
            transform.position = Vector3.Lerp(initialPosition, initialPosition + Vector3.up * initialMoveSpeed, elapsedTime / initialMoveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Transition to the Fired state
        currentState = TorpedoState.Fired;
        HandleFired();
    }

    private void HandleFired()
    {
        // Once in the Fired state, move towards the target position
        StartCoroutine(MoveTowardsTarget());
    }

    private IEnumerator MoveTowardsTarget()
    {
        while (currentState == TorpedoState.Fired)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            rb.velocity = direction * moveSpeed;

            // Check if close to the target position
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentState = TorpedoState.Contact;
                HandleContact();
            }

            yield return null;
        }
    }

    private void HandleContact()
    {
        // Stop the torpedo
        rb.velocity = Vector3.zero;

        // Add effects or damage logic here for when the torpedo hits the target
        // For example, you could play an explosion animation or deal damage to the player

        // Destroy the torpedo after contact
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the torpedo hits something (e.g., the player or an obstacle)
        if (currentState == TorpedoState.Fired)
        {
            currentState = TorpedoState.Contact;
            HandleContact();
        }
    }
}
