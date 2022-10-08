using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ThirdPersonShooter.Ai
{
    public class AiAgentDeathEffect : MonoBehaviour
    {
        AiAgent agent;
        public GameObject minimap_follow;
        
        void Start()
        {
            agent = GetComponent<AiAgent>(); 
        }

        public void AgentDead(Vector3 direction)
        {
            AiDeathState deathState = agent.stateMachine.GetState(AiStateId.Death) as AiDeathState;
            deathState.direction = direction;
            agent.stateMachine.ChangeState(AiStateId.Death);
        }
    }
}