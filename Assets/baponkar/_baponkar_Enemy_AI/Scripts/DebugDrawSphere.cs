using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ThirdPersonShooter.Ai
{
public class DebugDrawSphere : MonoBehaviour
{
    public bool debug;
    public float radius = 0.1f;
    public void OnDrawGizmos()
    {
        if(debug)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}
}