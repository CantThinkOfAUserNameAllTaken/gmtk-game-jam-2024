using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;



public class movement : MonoBehaviour
{
    
    public float speed;
    private Vector2 directions;
    public Rigidbody2D rb;

    public Vector2 mouse_dir;
    public GameObject light_glow;
    public InputActionReference inpt;

    public PlayerShrinking pl_sh;

    void Start()
    {
        

    }

    public void Update()
    {
        directions = inpt.action.ReadValue<Vector2>();

        mouse_dir = Camera.main.ScreenToWorldPoint(Input.mousePosition);

       

       
       
       
    }

    public void FixedUpdate()
    {
        rb.velocity = new Vector2(directions.x, directions.y) * speed;

        Vector2 look_Dir = mouse_dir - rb.position;
        float angle = Mathf.Atan2(look_Dir.y, look_Dir.x) * Mathf.Rad2Deg - 180f;
        light_glow.transform.rotation = Quaternion.Euler(new Vector3(0,0, angle));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Victory");
        }
    }
}
