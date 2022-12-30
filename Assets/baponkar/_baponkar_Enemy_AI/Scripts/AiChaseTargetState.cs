using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ThirdPersonShooter.Ai
{
    public class AiChaseTargetState : AiState
    {
        float timer = 0.0f;

        public AiStateId GetStateId()
        {
            return AiStateId.ChaseTarget;
        }
        
        public void Enter(AiAgent agent)
        {
            agent.navMeshAgent.stoppingDistance = 5.0f;
            agent.navMeshAgent.speed = agent.config.runningSpeed;
            agent.navMeshAgent.stoppingDistance = 5f;
            agent.navMeshAgent.isStopped = false;
            agent.navMeshAgent.SetDestination(agent.targetingSystem.TargetPosition);
        }
        
        public void Update(AiAgent agent)
        {
            if(!agent.enabled)
            {
                return;
            }

            timer -= Time.deltaTime;

            if(!agent.navMeshAgent.hasPath)
            {
                agent.navMeshAgent.SetDestination(agent.targetingSystem.TargetPosition);
            }

            if(timer <= 0 && agent.targetingSystem.HasTarget)
            {
                Vector3 direction = agent.targetingSystem.TargetPosition - agent.transform.position;
                direction.y = 0;
                if(direction.sqrMagnitude > agent.config.maxDistance * agent.config.maxDistance)
                {
                    if(agent.navMeshAgent.pathStatus != NavMeshPathStatus.PathPartial)
                    {
                        agent.navMeshAgent.SetDestination(agent.targetingSystem.TargetPosition);
                        agent.weapons.SetFireing(true);
                    }
                }
                timer = agent.config.maxTime;
            }
            else if(timer <= 0 && !agent.targetingSystem.HasTarget)
            {
                agent.stateMachine.ChangeState(AiStateId.FindTarget);
                timer = agent.config.maxTime;
            }

            if(agent.targetingSystem.HasTarget && agent.targetingSystem.TargetDistance <= agent.config.attackRange)
            {
                agent.stateMachine.ChangeState(AiStateId.AttackTarget);
            }
            
        }
        
        public void Exit(AiAgent agent)
        {
            agent.navMeshAgent.stoppingDistance = 0.0f;
        }
        
    }
}