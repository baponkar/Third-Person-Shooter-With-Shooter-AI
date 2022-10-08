using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ThirdPersonShooter.Ai
{
    [CreateAssetMenu(fileName = "AiAgentConfig", menuName = "Ai Agent Configaration", order = 1)]
    public class AiAgentConfig : ScriptableObject
    {
        [Header("Find Locomotion Speed Parameters.")]
        public float walkingSpeed = 2.11474f;
        public float runningSpeed = 4.229481f;

        [Header("Patrol Parameters.")]
        public float patrolWaitTime = 1f;
        public float patrolRadius = 10f;
        public float patrolTurnSpeed = 720f;

        [Header("Flee Parameters.")]
        public float fleeRadius = 20f;
        

        [Header("Attack State Parameters.")]
        public float attackRange = 25f;

        public float maxTime  = 1.0f;
        public float maxDistance = 2.0f;
        public float dieForce = 10f;
        public float maxSightDistance = 5.0f;
        public float turnSpeed = 720f;

    }
}