using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseHealth : MonoBehaviour
{



    [SerializeField]
    protected int MaxHealth;

    protected int CurrentHealth;

    public delegate void Event();

    public Event OnHealthChanged;

    public Event OnHealthIncrease;

    public Event OnHealthDecrease;

    [SerializeField]
    private AudioList _audioList;
    // Start is called before the first frame update

    // Start is called before the first frame update
    protected virtual void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public virtual void DamageTaken(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Death();
        }
        OnHealthChanged();
        OnHealthDecrease();
    }

    public void Heal(int healAmount)
    {
        CurrentHealth += healAmount;
        OnHealthChanged();
        OnHealthIncrease();
    }

    protected virtual void Death()
    {
        _audioList.PlaySound("Death", gameObject);
    }



        // Update is called once per frame
        void Update()
    {
        
    }
}
