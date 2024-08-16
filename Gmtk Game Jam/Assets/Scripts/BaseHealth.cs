using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHealth : MonoBehaviour
{



    [SerializeField]
    private int _maxHealth;

    private int _currentHealth;

    public delegate void OnHealthChange();

    public OnHealthChange OnHealthChanged;
    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    void DamageTaken(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            Death();
        }
        OnHealthChanged();
    }

    public abstract void Death();
    

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
