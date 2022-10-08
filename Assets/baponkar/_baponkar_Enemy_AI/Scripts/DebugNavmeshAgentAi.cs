using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace ThirdPersonShooter.Ai
{
public class DebugNavmeshAgentAi : MonoBehaviour
{
    public bool velocity;
    public bool desiredVelocity;
    public bool path;

    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
       agent = GetComponent<NavMeshAgent>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        if(velocity)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + agent.velocity);
        }
        if(desiredVelocity)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + agent.desiredVelocity);
        }
        if(path)
        {
            Gizmos.color = Color.black;
            if(agent.path != null)
            {
                Gizmos.color = Color.black;
                var agentPath = agent.path;
                Vector3 prevCorner = transform.position;

                foreach(var corner in agentPath.corners){
                    Gizmos.DrawLine(prevCorner,corner);
                    Gizmos.DrawSphere(corner,0.1f);
                    prevCorner = corner;
                }
            }
        }
    }
}
}