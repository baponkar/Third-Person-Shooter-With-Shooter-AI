using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClimbing : MonoBehaviour
{
    public float wallAngleMax;
    public float overpassHeight;

    public Vector3 climbOriginDown;

    public bool climbing;

    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       if(climbing)
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
       else
        {
            rb.useGravity = true;
        }
    }
}
