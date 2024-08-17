using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShrinking : MonoBehaviour
{
    [SerializeField]
    private Vector3 _shrinkSize;
    
    private PlayerInput _playerInput;

    private delegate void Event();

    private Event _currentState;

    [SerializeField]
    private float _shrinkSpeed;


    private Vector3 _originalSize;

    private bool _keyPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _originalSize = transform.localScale;
        _currentState = Grown;
    }

    private void Shrinking()
    {
        LerpToSize(_shrinkSize, transform.localScale);
        if (transform.localScale == _shrinkSize)
        {
            _keyPressed = false;
            _currentState = Shrunk;
        }
    }

    void Shrunk()
    {
        _currentState = Growing;
    }

    private void Growing()
    {
        LerpToSize(_originalSize, transform.localScale);
        if (transform.localScale == _originalSize)
        {
            _keyPressed = false;
            _currentState = Grown;
        }
    }

    void Grown()
    {
        _currentState = Shrinking;
    }
    void LerpToSize(Vector3 toSize, Vector3 originalSize)
    {
        Vector3 newSize = Vector3.MoveTowards(originalSize, toSize, _shrinkSpeed * Time.deltaTime);
        transform.localScale = newSize;
    }

    public void OnShrinkCalled()
    {
        _keyPressed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_keyPressed)
        {
            _currentState();
        }

    }
}
