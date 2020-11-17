using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AstroidController : TakeDamage
{
    private enum asteroidSizes
    {
        BIG, MEDIUM, SMALL
    }
    
    [SerializeField] private asteroidSizes asteroidSize;
    public float minSpeed = 3f, maxSpeed = 10f;
    [SerializeField] private int scoreToPlayer = 10;
    [SerializeField] private int numberOfAsteroidsToSpawnAfterDeath = 2;
    [SerializeField] private UnityEvent onAsteroidExploded;
    

    private AsteroidsSpawner _asteroidsSpawner;

    private void Awake()
    {
        _asteroidsSpawner = AsteroidsSpawner.Instance;
    }

    private Vector2 _position, _velocity;
    protected override void OnExploding()
    {
        if (asteroidSize == asteroidSizes.BIG)
        {
            for (int i = 0; i < numberOfAsteroidsToSpawnAfterDeath; i++)
            {
                _asteroidsSpawner.SpawnSpecificAsteroid(1, transform.position);
            }
        }

        else if (asteroidSize == asteroidSizes.MEDIUM)
        {
            for (int i = 0; i < numberOfAsteroidsToSpawnAfterDeath; i++)
            {
                _asteroidsSpawner.SpawnSpecificAsteroid(2, transform.position);
            }
        }
        else if(asteroidSize == asteroidSizes.SMALL)
        {
            _asteroidsSpawner.CheckWaveEnding();
        }
        ShipManager.Instance.IncreaseScore(scoreToPlayer);
        onAsteroidExploded.Invoke();
        Destroy(gameObject);
    }

    public void Pause()
    {
        _position = transform.position;
        if (TryGetComponent(out Rigidbody2D rigidbody2D))
        {
            _velocity = rigidbody2D.velocity;
        }
        gameObject.SetActive(false);
    }

    public void Continue()
    {
        gameObject.SetActive(true);
        transform.position = _position;
        if (TryGetComponent(out Rigidbody2D rigidbody2D))
        {
            rigidbody2D.velocity = _velocity;
        }
    }
}
