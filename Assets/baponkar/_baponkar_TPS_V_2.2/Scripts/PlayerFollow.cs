using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerFollow : MonoBehaviour
{
    public float distanceToPlayer = 1.0f;
    NavMeshAgent agent;
    public Transform player;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
            agent.SetDestination(player.position);
            anim.SetFloat("inputX", agent.velocity.x);
            anim.SetFloat("inputY", agent.velocity.z);
        
       
    }
}
