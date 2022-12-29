/*This Health Script is common for Player and AI*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ThirdPersonShooter.Ai;


public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isDead = false;
    [HideInInspector] PlayerDeathEffect playerDeathEffect = null;
    [HideInInspector] AiAgentDeathEffect aiAgentDeathEffect = null;
    //[HideInInspector] AiAgent agent;
    public bool getShot = false;


    void Start()
    {
        currentHealth = maxHealth;

        var rigidbodies = GetComponentsInChildren<Rigidbody>();
        if(rigidbodies.Length > 0)
        {
            foreach(Rigidbody rb in rigidbodies)
            {
            HitBox hitbox =  rb.gameObject.AddComponent<HitBox>();
            hitbox.health = this;
            }
        }
        else
        {
            Debug.LogError("No Ragdol Rigidbodies found!");
        }
        
        if(transform.tag == "Player")
        {
            playerDeathEffect = GetComponent<PlayerDeathEffect>();
        }
        if(transform.tag == "Enemy")
        {
            aiAgentDeathEffect = GetComponent<AiAgentDeathEffect>();
        }

        //agent = GetComponent<AiAgent>();
    }

    // void Update()
    // {
    //     if(agent)
    //     {
    //         if(currentHealth < maxHealth)
    //         {
    //             agent.stateMachine.ChangeState(AiStateId.AttackTarget);
    //         }
    //     }
    // }

    IEnumerator GetShot()
    {
        getShot = true;
        yield return new WaitForSeconds(5f);
        getShot = false;
    }

    public void TakeDamage(float damage, Vector3 direction)
    {
        currentHealth -= damage;
        if (currentHealth <= 0.0f)
        {
            isDead = true;
            DeathEffect(direction);
        }

        StartCoroutine("GetShot");
    }

    public void DeathEffect(Vector3 direction)
    {
        if(playerDeathEffect)
        {
            playerDeathEffect.PlayerDead();
        }
        else if(aiAgentDeathEffect)
        {
            aiAgentDeathEffect.AgentDead(direction);
        }
    }
}
