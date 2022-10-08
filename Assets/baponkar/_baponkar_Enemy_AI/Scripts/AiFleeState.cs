using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ThirdPersonShooter.Ai
{
public class AiFleeState : AiState
{
    bool walkPointSet;
    Vector3 tempTarget;
    float timer;
    UnityEngine.AI.NavMeshPath navMeshPath;
    Vector3 initialPosition;
    
    public AiStateId GetStateId()
    {
        return AiStateId.Flee;
    }
    
    public void Enter(AiAgent agent)
    {
        initialPosition = agent.transform.position;
        navMeshPath = new UnityEngine.AI.NavMeshPath();
        agent.navMeshAgent.stoppingDistance = 0f;
    }
    
    public void Update(AiAgent agent)
    {
        
        float distance = Vector3.Distance(agent.transform.position, agent.playerTransform.position);
        if(distance >= agent.config.fleeRadius)
        {
            agent.navMeshAgent.isStopped = true;
            FacePlayer(agent);
        }
        else
        {
            Patrolling(agent);
            agent.navMeshAgent.isStopped = false;
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
        Vector3 tempPos = Vector3.zero;
        
        tempPos = RandomNavmeshLocation(agent);
        tempTarget = new Vector3(agent.navMeshAgent.transform.position.x + tempPos.x, agent.navMeshAgent.transform.position.y, agent.navMeshAgent.transform.position.z + tempPos.z);
        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(tempPos, out hit, 5 * agent.config.fleeRadius, UnityEngine.AI.NavMesh.AllAreas) )
        {
            if(agent.navMeshAgent.CalculatePath(hit.position, navMeshPath)) //check a path available or not
            {
                if(Vector3.Distance(hit.position,agent.playerTransform.position) > agent.config.fleeRadius)
                {
                    tempTarget = hit.position;
                    walkPointSet = true;
                }
            }
        }
        else
        {
            tempTarget = agent.transform.position;
            walkPointSet = false;
        }
    }

     Vector3 RandomNavmeshLocation(AiAgent agent) {
        Vector3 randomDirection = Random.insideUnitSphere * agent.config.fleeRadius * 2f;
        randomDirection += agent.navMeshAgent.transform.position;
        UnityEngine.AI.NavMeshHit hit;
        Vector3 finalPosition = (Vector3) agent.navMeshAgent.transform.position;

        if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, agent.config.fleeRadius*2.0f, 1)) {
            float distance = Vector3.SqrMagnitude(initialPosition - hit.position);
            if( distance < agent.config.fleeRadius * agent.config.fleeRadius)
            {
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
        agent.navMeshAgent.transform.rotation = Quaternion.Lerp(agent.navMeshAgent.transform.rotation, lookRotation,Time.time * agent.config.patrolTurnSpeed);
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