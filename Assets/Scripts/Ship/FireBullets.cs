using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour
{
    [SerializeField] private GameObject bulletsPrefab;
    [SerializeField] private float bulletSpeed = 12, bulletLifeTime = 3;
    private Transform _firePosition;
    private InputManager _inputManager;

    private void Awake()
    {
        _inputManager = InputManager.Instance;
        _firePosition = GetComponentInChildren<FirePosition>().transform;
    }

    private void Update()
    {
        if (_inputManager.Fire)
        {
            InstantiateBullet();
        }
    }

    private void InstantiateBullet()
    {
        GameObject newBullet = Instantiate(bulletsPrefab);
        newBullet.transform.position = _firePosition.position;
        newBullet.transform.up = transform.up;
        if (newBullet.TryGetComponent(out Rigidbody2D rigidbody2D))
        {
            rigidbody2D.velocity = transform.up * bulletSpeed;
        }
        
        Destroy(newBullet, bulletLifeTime);
    }
}
