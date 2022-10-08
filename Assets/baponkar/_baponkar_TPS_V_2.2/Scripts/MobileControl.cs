using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControl : MonoBehaviour
{
    [HideInInspector] public float horizontal;
    [HideInInspector] public float vertical;
    [HideInInspector] public bool jump;
    [HideInInspector] public bool sprint;
    [HideInInspector] public bool crouch;
    [HideInInspector] public bool fire;
    [HideInInspector] public bool aim;

    public Joystick moveJoystick;
    public Joystick mouseJoystick;

    
    void Start()
    {
        
    }

    
    void Update()
    {
        horizontal = moveJoystick.Horizontal;
        vertical = moveJoystick.Vertical;
    }
}
