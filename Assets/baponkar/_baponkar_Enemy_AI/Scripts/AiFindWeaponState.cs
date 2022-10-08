using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ThirdPersonShooter.Ai
{
public class AiFindWeaponState : AiState
{
    public AiStateId GetStateId()
    {
        return AiStateId.FindWeapon;
    }
    
    public void Enter(AiAgent agent)
    {
        WeaponPickup pickup = FindClosestWeapon(agent);
        agent.navMeshAgent.speed = agent.config.runningSpeed;
        agent.navMeshAgent.stoppingDistance = 0.0f;
        agent.navMeshAgent.SetDestination(pickup.transform.position);
    }
    
    public void Update(AiAgent agent)
    {
        if(agent.weapons.HasWeapon())
        {
            agent.stateMachine.ChangeState(AiStateId.FindTarget);
        }
        else
        {
            WeaponPickup pickup = FindClosestWeapon(agent);
            if(pickup)
            {
                if(agent.navMeshAgent.remainingDistance <= 0.1f)
                {
                    agent.navMeshAgent.SetDestination(pickup.transform.position);
                } 
            }
            else
            {
                agent.stateMachine.ChangeState(AiStateId.Patrol);
            }
        }
    }
    
    public void Exit(AiAgent agent)
    {
        
    }

    private WeaponPickup FindClosestWeapon(AiAgent agent)
    {
        WeaponPickup [] weaponPickups = GameObject.FindObjectsOfType<WeaponPickup>();
        WeaponPickup closestWeapon = null;
        float closestDistance = float.MaxValue;
        foreach(WeaponPickup weaponPickup in weaponPickups)
        {
            float distance = Vector3.Distance(weaponPickup.transform.position ,agent.transform.position);
            if(distance < closestDistance)
            {
                closestDistance = distance;
                closestWeapon = weaponPickup;
            }
        }
        
        return closestWeapon;
    }
}
}