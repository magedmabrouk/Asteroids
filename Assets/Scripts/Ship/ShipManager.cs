using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipManager : TakeDamage
{
    [SerializeField] private int lives = 10;
    [SerializeField] private int score = 0;
    [SerializeField] private UnityEvent onShipExplosion;
    [SerializeField] private GameObject explosionEffect;
    
    private static ShipManager _instance;
    public static ShipManager Instance {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ShipManager>();
            }

            return _instance;
        }
    }
    

    protected override void OnExploding()
    {
        DecreaseLives(1);
        ResetHP();
        if (explosionEffect)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
        onShipExplosion.Invoke();
        GameManager.Instance.ShipExploded();
    }

    public void DecreaseLives(int amount)
    {
        lives -= amount;
        UIManager.Instance.SetLives(lives);
    }
    
    public void IncreaseLives(int amount)
    {
        lives += amount;
        UIManager.Instance.SetLives(lives);
    }

    public int GetLives => lives;
    
    public void DecreaseScore(int amount)
    {
        score -= amount;
        UIManager.Instance.SetScore(score);
    }
    
    public void IncreaseScore(int amount)
    {
        score += amount;
        UIManager.Instance.SetScore(score);
    }

    public int GetScore => score;
}
