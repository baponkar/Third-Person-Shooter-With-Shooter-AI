using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace ThirdPersonShooter.Ai
{
    public class AiAttackTargetState : AiState
    {
        public  AiStateId GetStateId()
        {
            return AiStateId.AttackTarget;
        }

        public  void Enter(AiAgent agent)
        {
            agent.FaceTowardTarget(agent.targetingSystem.TargetPosition);
            agent.weapons.ActivateWeapon();
            agent.navMeshAgent.stoppingDistance = agent.config.attackRange;
            agent.navMeshAgent.isStopped = true;
        }

        public void Update(AiAgent agent)
        {
            if(agent.health.isDead)
            {
                agent.stateMachine.ChangeState(AiStateId.Death);
            }
            else
            {
                if(agent.targetingSystem.HasTarget)
                {
                    if(agent.targetingSystem.TargetDistance <= agent.config.attackRange)
                    {
                        agent.FaceTowardTarget(agent.targetingSystem.TargetPosition);
                        agent.weapons.SetFireing(true);
                    }
                    else
                    {
                        agent.weapons.SetFireing(false);
                        agent.stateMachine.ChangeState(AiStateId.ChaseTarget);
                    }
                }
                else
                {
                    agent.stateMachine.ChangeState(AiStateId.FindTarget);
                }

            }
            
            
        }
        
        public void Exit(AiAgent agent)
        {
            agent.navMeshAgent.stoppingDistance = 0.0f;
            agent.weapons.DeActivateWeapon();
            agent.navMeshAgent.isStopped = false;
        }
    }
}