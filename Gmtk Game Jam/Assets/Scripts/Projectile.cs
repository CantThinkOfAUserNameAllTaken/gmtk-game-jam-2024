using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{

    [SerializeField]
    private string[] _tagsToHit;

    [SerializeField]
    private int _damage;

    [SerializeField]
    private float _maxSpeed;

    [SerializeField]
    private float _lifeTime;

    private float _currantSpeed = 0;

    [SerializeField]
    private float _rampUpSpeed;

    private Rigidbody2D _rb;

    private Vector2 _fireDirection;

  
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Invoke("DestroyBullet", _lifeTime);
        Vector2 mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _fireDirection = mouseDirection - (Vector2)transform.position;
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsTagInArray(collision.gameObject.tag))
        {
            GetComponent<BaseHealth>().DamageTaken(_damage);
        }
        Destroy(gameObject);
    }

    private bool IsTagInArray(string Intag)
    {
        foreach (string tag in _tagsToHit)
        {
            if (tag == Intag)
            {
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _currantSpeed = Mathf.Lerp(_currantSpeed, _maxSpeed, _rampUpSpeed * Time.deltaTime);
        _rb.velocity = _fireDirection * _currantSpeed * Time.fixedDeltaTime;
    }
}
