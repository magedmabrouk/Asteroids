using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TakeDamage:MonoBehaviour
{
    [SerializeField] private int health = 10;

    private int _startingHP;

    private void Start()
    {
        _startingHP = health;
    }

    public void TakeDamageAmount(int damage)
    {
        health -= damage;
        OnTakingDamage();
        if (health <= 0)
        {
            OnExploding();
        }
    }

    public void TakeDestructiveDamage()
    {
        health = 0;
        OnTakingDamage();
        OnExploding();
    }

    protected virtual void OnExploding()
    {
        
    }

    protected virtual void OnTakingDamage()
    {
        
    }

    protected void ResetHP()
    {
        health = _startingHP;
    }
    
    
}
