using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBoundaries : MonoBehaviour
{
    private static ScreenBoundaries _instance;  
    public static ScreenBoundaries Instance {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<ScreenBoundaries>();
            }
            if (!_instance)
            {
                _instance = CreateScreenBoundaries();
            }
            return _instance;
        }
    }
    private Vector2 _boundaries;

    private void Awake()
    {
        _boundaries = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        
        BoxCollider2D _collider;
        if (TryGetComponent(out _collider))
        {
            _collider.size = _boundaries*2;
        }
    }

    private static ScreenBoundaries CreateScreenBoundaries()
    {
        GameObject SB = new GameObject();
        SB.name = "ScreenBoundaries";
        SB.transform.position = Vector3.zero;
        SB.transform.localScale = Vector3.one;
        BoxCollider2D boxCollider2D = SB.AddComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
        ScreenBoundaries screenBoundaries = SB.AddComponent<ScreenBoundaries>();
        return screenBoundaries;
    }

    public Vector2 GetBoundaries() => _boundaries;
}
