using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ThirdPersonShooter.Ai
{
    public class AiSniperState : AiState
    {
        
        public  AiStateId GetStateId()
        {
            return AiStateId.Sniper;
        }
        
        public  void Enter(AiAgent agent)
        {
            agent.navMeshAgent.isStopped = true;
        }
        
        public  void Update(AiAgent agent)
        {
            if(agent.weapons.HasWeapon())
            {
                if(agent.targetingSystem.HasTarget)
                {
                    agent.FaceTowardTarget(agent.targetingSystem.TargetPosition);
                    agent.weapons.SetFireing(true);
                }
                else
                {
                    agent.weapons.SetFireing(false);
                }

                if(agent.health.getShot)
                {
                    agent.FaceTowardTarget(agent.playerTransform.position);
                }
            }
            // else
            // {
            //     agent.stateMachine.ChangeState(AiStateId.FindWeapon);
            // }
        }
        
        public void Exit(AiAgent agent)
        {
            agent.navMeshAgent.isStopped = false;
        }
    }
}