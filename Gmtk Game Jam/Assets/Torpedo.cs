using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : MonoBehaviour
{

    [SerializeField]
    private float _initialMoveTime = 0.5f;
    [SerializeField]
    private float _initialMoveSpeed = 0.5f;
    [SerializeField]
    private float _moveSpeed = 5f;
    [SerializeField]
    private float _rotateSpeed = 1f;
    [SerializeField]
    private float _lifeSpan = 5f; 

    private Rigidbody2D _rb;
    private Vector3 _targetPosition;
    private bool _isLockedOn; 

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        GetTargetPosition();

        _isLockedOn = false;
        StartCoroutine(InitialMovement());
    }
    public void GetTargetPosition()
    {
        _targetPosition = FindObjectOfType<PlayerHealth>().gameObject.transform.position;
    }

    private IEnumerator InitialMovement()
    {
        if (!_isLockedOn)
        {
            float elapsedTime = 0f;

            while (elapsedTime < _initialMoveTime)
            {
                _rb.velocity = Vector2.up * _initialMoveSpeed;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Stop the upward movement and lock onto the target
            _rb.velocity = Vector2.zero;
            _isLockedOn = true;
        }
    }
    private void FixedUpdate()
    {
        if (_isLockedOn)
        {
            GetTargetPosition();
            Vector2 direction = (Vector2)_targetPosition - _rb.position;

            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;
            _rb.angularVelocity = -rotateAmount * _rotateSpeed;

            _rb.velocity = transform.up * _moveSpeed;
        }

    }

    void OnTriggerEnter2D()
    {
        Destroy(gameObject);
    }
}
