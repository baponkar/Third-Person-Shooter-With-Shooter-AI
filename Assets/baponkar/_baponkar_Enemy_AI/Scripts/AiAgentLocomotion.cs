using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace ThirdPersonShooter.Ai
{
    public class AiAgentLocomotion : MonoBehaviour
    {
        NavMeshAgent agent;
        Animator anim;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            if(agent.hasPath)
            {
                anim.SetFloat("Speed", agent.velocity.magnitude); 
            }
            else
            {
                anim.SetFloat("Speed", 0);
            }
        }
    }
}