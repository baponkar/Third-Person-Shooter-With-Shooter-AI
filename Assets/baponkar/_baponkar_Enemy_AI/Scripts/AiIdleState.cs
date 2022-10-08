using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ThirdPersonShooter.Ai
{
public class AiIdleState : AiState
{
    public  AiStateId GetStateId()
    {
        return AiStateId.Idle;
    }
    
    public  void Enter(AiAgent agent)
    {
        
    }
    
    public void Update(AiAgent agent)
    {
        Vector3 playerDirection = agent.playerTransform.position - agent.transform.position;
        if(playerDirection.sqrMagnitude > agent.config.maxSightDistance * agent.config.maxSightDistance)
        {
            return;
        }

        Vector3 agentDirection = agent.transform.forward;
        playerDirection.Normalize();
        float dotProduct = Vector3.Dot(agentDirection, playerDirection);
        if(dotProduct > 0)
        {
            agent.stateMachine.ChangeState(AiStateId.ChaseTarget);
        }
    }
    
    public void Exit(AiAgent agent)
    {
        
    }
}

}