using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairTarget : MonoBehaviour
{
    public float smallDistance = 4.0f;
    public Vector3 offsetDirection;
    Camera mainCamera;
    Ray ray;
    RaycastHit hitInfo;
    public Color color;
    public LayerMask layerMask;

    void Start()
    {
       mainCamera = Camera.main; 
    }

    
    void Update()
    {
        //In every frame this node transform update by main camera.
        ray.origin = mainCamera.transform.position;
        ray.direction = mainCamera.transform.forward;

        if(Physics.Raycast(ray, out hitInfo,layerMask)) // This help to aim along sky
        {
        
            if(Vector3.Distance(ray.origin, hitInfo.point) > smallDistance)
            {
                transform.position = hitInfo.point;
            }
            else
            {
                transform.position = ray.origin + transform.forward * (smallDistance + 2f);
            }
        }
      
        //Debug.Log(Vector3.Distance(ray.origin, hitInfo.point));
    }
    void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
