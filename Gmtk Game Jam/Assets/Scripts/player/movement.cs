using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;



public class movement : MonoBehaviour
{
    
    public float speed;
    private Vector2 directions;
    public Rigidbody2D rb;

    void Start()
    {
        

    }

    public void UpdateMovemenntDirection(CallbackContext context)
    {
        directions = context.ReadValue<Vector2>();
    }

    public void FixedUpdate()
    {
        rb.velocity = new Vector2(directions.x, directions.y) * speed;
    }
}
