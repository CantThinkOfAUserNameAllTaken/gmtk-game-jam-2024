using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShrinking : MonoBehaviour
{
    [SerializeField]
    private Vector3 _shrinkSize;
    [SerializeField]
    private Vector3 _shrinkSize_light_sourse;

    private PlayerInput _playerInput;

    private delegate void Event();

    private Event _currentState;

    [SerializeField]
    private float _shrinkSpeed;


    private Vector3 _originalSize;
    private Vector3 _originalSize_light_sourse;

    private bool _keyPressed = false;

    public GameObject light_beam;

    [SerializeField]
    private GameEvent<int> _onShrunk;

    [SerializeField]
    private GameEvent<int> _onGrown;

    [SerializeField]
    LayerMask _layerToHit;

    public movement mov;
    // Start is called before the first frame update
    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _originalSize = transform.localScale;
        _originalSize_light_sourse = light_beam.transform.localScale;
        _currentState = Grown;
    }

    private void Shrinking()
    {
        LerpToSize(_shrinkSize, transform.localScale);
        Light_LerpSize(_shrinkSize_light_sourse, light_beam.transform.localScale);
        mov.speed = 30;
        if (transform.localScale == _shrinkSize)
        {
            
                if(light_beam.transform.localScale == _shrinkSize_light_sourse)
                {
                    _onShrunk.Raise(0);
                    _keyPressed = false;
                    _currentState = Shrunk;
                }
                
            
            
        }
    }

    void Shrunk()
    {

        _currentState = Growing;
        
    }

    private void Growing()
    {
        LerpToSize(_originalSize, transform.localScale);
        Light_LerpSize(_originalSize_light_sourse, light_beam.transform.localScale);
        mov.speed = 5;
        if (transform.localScale == _originalSize)
        {
           
                if(light_beam.transform.localScale == _originalSize_light_sourse)
                {
                    _onGrown.Raise(0);
                    _keyPressed = false;
                    _currentState = Grown;
                }
            
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

    void Light_LerpSize(Vector3 light_tosize, Vector3 light_originalSize)
    {
        float speed_plus = 100;


         Vector3 newSize_light = Vector3.MoveTowards(light_originalSize, light_tosize, _shrinkSpeed * speed_plus * Time.deltaTime);
            light_beam.transform.localScale = newSize_light;
    }

    public void OnShrinkCalled()
    {
        Collider2D hit = Physics2D.OverlapBox(transform.position, _originalSize, 0, _layerToHit);

        if (!hit)
        {
            _keyPressed = true;
        }
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
