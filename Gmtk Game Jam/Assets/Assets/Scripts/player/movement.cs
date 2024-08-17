using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class movement : MonoBehaviour
{
    
    public float speed;
    public InputActionReference inpt;
    private Vector2 directions;
    public Rigidbody2D rb;

    void Start()
    {
        

    }
    public void Update()
    {
        directions = inpt.action.ReadValue<Vector2>();
    }

    public void FixedUpdate()
    {
        rb.velocity = new Vector2(directions.x, directions.y) * speed;
    }

    public void make_a_move(Vector2 move)
    {
        Debug.Log(move);
    }
}
