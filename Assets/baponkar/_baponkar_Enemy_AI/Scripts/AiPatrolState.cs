using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace ThirdPersonShooter.Ai
{
public class AiPatrolState : AiState
{  
    bool walkPointSet;
    Vector3 tempTarget;
    float timer;
    UnityEngine.AI.NavMeshPath navMeshPath;
    Vector3 initialPosition;
    
    RandomPointOnNavMesh randomPointOnNavMesh;
         
    public AiStateId GetStateId()
    {
        return AiStateId.Patrol;
    }

    public void Enter(AiAgent agent)
    {
        randomPointOnNavMesh = new RandomPointOnNavMesh();
        navMeshPath = new NavMeshPath();
        initialPosition = agent.transform.position;
        agent.navMeshAgent.stoppingDistance = 0.0f;
    }
    public void Update(AiAgent agent)
    {
        if(agent.health.isDead)
        {
            agent.stateMachine.ChangeState(AiStateId.Death);
        }
        else
        {
            if(agent.targetingSystem.HasTarget && agent.weapons.HasWeapon())
            {
                agent.stateMachine.ChangeState(AiStateId.AttackTarget);
            }
            else if(!agent.targetingSystem.HasTarget && agent.weapons.HasWeapon())
            {
                agent.stateMachine.ChangeState(AiStateId.FindTarget);
            }
            else
            {
                Patrolling(agent);
            }

            if(agent.health.getShot)
            {
                FacePlayer(agent);
                //agent.stateMachine.ChangeState(AiStateId.);
            }
        }
    } 
    public void Exit(AiAgent agent)
    {

    }

    void Patrolling(AiAgent agent)
    {
        timer -= Time.deltaTime;
        if(!walkPointSet)
        {
            SearchingPoint(agent);
        }
        if(walkPointSet && timer < 0f)
        {
            FacePatrol(agent);
            agent.navMeshAgent.speed = agent.config.walkingSpeed;
            agent.navMeshAgent.SetDestination(tempTarget);
            timer = agent.config.patrolWaitTime;
        }
        

        float distanceToTempTarget = agent.navMeshAgent.remainingDistance;
        if(distanceToTempTarget <= agent.navMeshAgent.stoppingDistance + 0.01f)
        {
            walkPointSet = false;
        }
    }
    void SearchingPoint(AiAgent agent)
    {
        Vector3 tempPos;
        
        if (randomPointOnNavMesh.RandomPoint(agent.transform.position,agent.config.patrolRadius,out tempPos))
        {
            tempTarget = tempPos;
            walkPointSet = true;
        }
        else
        {
            walkPointSet = false;
        }
    }

    

    void FacePatrol(AiAgent agent)
    {   
        Vector3 direction = (tempTarget- agent.navMeshAgent.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3 (direction.x,0,direction.z));
        agent.navMeshAgent.transform.rotation = Quaternion.Lerp(agent.navMeshAgent.transform.rotation, lookRotation,Time.time*agent.config.patrolTurnSpeed);
    }

    void FacePlayer(AiAgent agent)
    {   
        Vector3 direction = (agent.playerTransform.position - agent.navMeshAgent.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3 (direction.x,0,direction.z));
        agent.navMeshAgent.transform.rotation = Quaternion.Lerp(agent.navMeshAgent.transform.rotation, lookRotation,Time.time*agent.config.patrolTurnSpeed);
    }

    bool findThePlayer(AiAgent agent)
    {
         for (int i=0; i < agent.visonSensor.Objects.Count;i++)
         {
            if(agent.visonSensor.Objects[i].tag == "Player")
            {
                //playerSeen = true;
                return true;
            }
         }
        return false;
    }

}
}
