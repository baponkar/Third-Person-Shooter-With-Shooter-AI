using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace ThirdPersonShooter.Ai
{
public class AiFindTargetState : AiState
{
    
    
    bool walkPointSet;
    Vector3 tempTarget;
    float timer;
    UnityEngine.AI.NavMeshPath navMeshPath;
    Vector3 initialPosition;

    public  AiStateId GetStateId()
    {
        return AiStateId.FindTarget;
    }

    public void Enter(AiAgent agent)
    {
      navMeshPath = new NavMeshPath();
      initialPosition = agent.transform.position;
      agent.navMeshAgent.stoppingDistance = 0.0f;
    }

    public void Update(AiAgent agent)
    {
      if(agent.targetingSystem.HasTarget)
      {
            FaceTarget(agent, agent.targetingSystem.TargetPosition);
            agent.stateMachine.ChangeState(AiStateId.AttackTarget);
      }
      else
      {
        Patrolling(agent);
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
            timer = agent.config.maxTime;
        }
        

        float distanceToTempTarget = agent.navMeshAgent.remainingDistance;
        if(distanceToTempTarget <= agent.navMeshAgent.stoppingDistance + 0.01f)
        {
            walkPointSet = false;
        }
    }
    void SearchingPoint(AiAgent agent)
    {
        Vector3 tempPos = Vector3.zero;
        
        tempPos = RandomNavmeshLocation(agent);
        tempTarget = new Vector3(agent.navMeshAgent.transform.position.x + tempPos.x, agent.navMeshAgent.transform.position.y, agent.navMeshAgent.transform.position.z + tempPos.z);
        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(tempPos, out hit, 0.1f, UnityEngine.AI.NavMesh.AllAreas) )
        {
            if(agent.navMeshAgent.CalculatePath(hit.position, navMeshPath)) //check a path available or not
            {
                tempTarget = hit.position;
                walkPointSet = true;
            }
        }
        else
        {
            tempTarget = agent.transform.position;
            walkPointSet = false;
        }
    }

     Vector3 RandomNavmeshLocation(AiAgent agent) {
        Vector3 randomDirection = Random.insideUnitSphere * agent.config.patrolRadius;
        randomDirection += agent.navMeshAgent.transform.position;
        UnityEngine.AI.NavMeshHit hit;
        Vector3 finalPosition = (Vector3) agent.navMeshAgent.transform.position;

        if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, agent.config.patrolRadius, 1)) {
            float distance = Vector3.SqrMagnitude(initialPosition - hit.position);
            if( distance < agent.config.patrolRadius * agent.config.patrolRadius){
            finalPosition = hit.position;
            walkPointSet = true;
            }
        }
        return finalPosition;
     }

    void FacePatrol(AiAgent agent)
    {   
        Vector3 direction = (tempTarget- agent.navMeshAgent.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3 (direction.x,0,direction.z));
        agent.navMeshAgent.transform.rotation = Quaternion.Lerp(agent.navMeshAgent.transform.rotation, lookRotation,Time.time*agent.config.patrolTurnSpeed);
    }

    void FaceTarget(AiAgent agent, Vector3 target)
    {
        Vector3 direction = (target- agent.navMeshAgent.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3 (direction.x,0,direction.z));
        agent.navMeshAgent.transform.rotation = Quaternion.Lerp(agent.navMeshAgent.transform.rotation, lookRotation,Time.time*agent.config.patrolTurnSpeed);
    }
}
}