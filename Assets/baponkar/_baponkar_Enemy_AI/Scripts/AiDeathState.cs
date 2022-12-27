using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ThirdPersonShooter.Ai
{
public class AiDeathState : AiState
{
    public Vector3 direction;
    public AiStateId GetStateId()
    {
        return AiStateId.Death;
    }
    
    public void Enter(AiAgent agent)
    {
        agent.health.isDead = true;
        agent.navMeshAgent.isStopped = true;
        agent.ragdol.ActivateRagdol();
        direction.y = 1f;
        agent.ragdol.ApplyForce(direction * agent.config.dieForce);
        agent.weapons.SetFireing(false);
        agent.weapons.DropWeapon();
        

        // Destroy(gameObject);
        agent.transform.GetComponent<AiAgent>().enabled = false;
        agent.transform.GetComponent<AiAgentLocomotion>().enabled = false;
        agent.transform.GetComponent<AiWeapons>().enabled = false;
        agent.transform.GetComponent<AiVisonSensor>().enabled = false;
    }
    
    public void Update(AiAgent agent)
    {
       
    }
    
    public void Exit(AiAgent agent)
    {
        
    }
}
}