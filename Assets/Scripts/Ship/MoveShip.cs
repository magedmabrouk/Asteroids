using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveShip : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3f, rotationSpeed = 3f;
    private Rigidbody2D _rigidbody;
    private InputManager _inputManager;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputManager = InputManager.Instance;
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        if (!_rigidbody)
        {
            return;
        }
        _rigidbody.velocity = new Vector2(_inputManager.Horizontal, _inputManager.Vertical) * movementSpeed;
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.back, _inputManager.RightRotation * rotationSpeed, Space.World);
        transform.Rotate(Vector3.forward, _inputManager.LeftRotation * rotationSpeed, Space.World);
    }
}
