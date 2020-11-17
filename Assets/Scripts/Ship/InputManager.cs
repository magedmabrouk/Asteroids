using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float Horizontal => Input.GetAxis("Horizontal");
    public float Vertical => Input.GetAxis("Vertical");
    public float LeftRotation => Input.GetAxis("LeftRotation");
    public float RightRotation => Input.GetAxis("RightRotation");
    
    public bool Fire => Input.GetButtonDown("Jump");
    private static InputManager _instance;
    public static InputManager Instance {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InputManager>();
            }

            return _instance;
        }
    }
}
