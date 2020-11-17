using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoundariesControl : MonoBehaviour
{
    [SerializeField] private bool backToCenter;
    [SerializeField] private UnityEvent onOutOfBoundaries;
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.GetComponent<ScreenBoundaries>())
        {
            return;
        }
        
        onOutOfBoundaries.Invoke();
        if (backToCenter)
        {
            transform.position = Vector3.zero;
        }
        else
        {
            Vector3 closestBoundaryPoint = other.ClosestPoint(transform.position);
            transform.position = other.bounds.center - closestBoundaryPoint;   
        }
    }
}
