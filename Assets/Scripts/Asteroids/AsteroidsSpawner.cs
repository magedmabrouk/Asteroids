using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroidsPrefabs;
    private Vector2 _boundaries;
    private Vector4[] _instantiationRegions;
    
    
    public static AsteroidsSpawner Instance {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AsteroidsSpawner>();
            }

            return _instance;
        }
    }

    private static AsteroidsSpawner _instance;
    private void Awake()
    {
        _boundaries = ScreenBoundaries.Instance.GetBoundaries();
        SetInstantiationRegion();
    }

    private void SetInstantiationRegion()
    {
        _instantiationRegions = new Vector4[4];
        
        _instantiationRegions[0].x = -_boundaries.x - 1;
        _instantiationRegions[0].y = _boundaries.x + 1;
        _instantiationRegions[0].z = _boundaries.y + 1;
        _instantiationRegions[0].w = _boundaries.y;
        
        _instantiationRegions[1].x = _boundaries.x;
        _instantiationRegions[1].y = _boundaries.x + 1;
        _instantiationRegions[1].z = _boundaries.y;
        _instantiationRegions[1].w = -_boundaries.y;
        
        _instantiationRegions[2].x = -_boundaries.x - 1;
        _instantiationRegions[2].y = _boundaries.x + 1;
        _instantiationRegions[2].z = -_boundaries.y;
        _instantiationRegions[2].w = -_boundaries.y - 1;
        
        _instantiationRegions[3].x = -_boundaries.x - 1;
        _instantiationRegions[3].y = -_boundaries.x;
        _instantiationRegions[3].z = _boundaries.y;
        _instantiationRegions[3].w = -_boundaries.y;
    }

    private Vector2 GetInstantiationPos()
    {
        int region = Random.Range(0, 3);
        return new Vector2(Random.Range(_instantiationRegions[region].x, _instantiationRegions[region].y),
            Random.Range(_instantiationRegions[region].y, _instantiationRegions[region].w));
    }

    private Vector2 GetInstantiationDirection()
    {
        return new Vector2(Random.Range(_boundaries.x, -_boundaries.x), Random.Range(_boundaries.y, -_boundaries.y));
    }

    private void SpawnAsteroid(GameObject asteroid, Vector2 position)
    {
        GameObject newAsteroid = Instantiate(asteroid, transform);
        
        Vector2 direction = (GetInstantiationDirection() - position).normalized;
        newAsteroid.transform.position = position;
        newAsteroid.transform.up = direction;
        
        AstroidController astroidController = newAsteroid.GetComponent<AstroidController>();
        float speed = Random.Range(astroidController.minSpeed, astroidController.maxSpeed);
        newAsteroid.GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    public void SpawnWave(int waveMax, int waveMin)
    {
        int numberOfAsteroids = Random.Range(waveMin, waveMax);
        for (int i = 0; i < numberOfAsteroids; i++)
        {
            int randomPrefab = Random.Range(0, asteroidsPrefabs.Length - 1);
            Vector2 pos = GetInstantiationPos();
            SpawnAsteroid(asteroidsPrefabs[randomPrefab], pos);
        }
    }

    public void SpawnSpecificAsteroid(int prefabNumber, Vector2 instantiationPosition)
    {
        if (prefabNumber >= asteroidsPrefabs.Length)
        {
            return;
        }
        SpawnAsteroid(asteroidsPrefabs[prefabNumber], instantiationPosition);
    }

    public void CheckWaveEnding()
    {
        if (transform.childCount <= 1)
        {
            print("done1");
            GameManager.Instance.NextWave();
        }
    }

    public void DisableAsteroids()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).TryGetComponent(out AstroidController astroidController))
            {
                astroidController.Pause();
            }
        }
    }
    
    public void EnableAsteroids()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).TryGetComponent(out AstroidController astroidController))
            {
                astroidController.Continue();
            }
        }
    }
    
    
}
