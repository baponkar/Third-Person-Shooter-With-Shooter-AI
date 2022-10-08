using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace ThirdPersonShooter.Ai
{
    public class AiAgent : MonoBehaviour
    {
        #region Variables
        public AiStateMachine stateMachine;
        public AiStateId initialState;

        [HideInInspector] public NavMeshAgent navMeshAgent;
        [HideInInspector] public Transform playerTransform;

        [HideInInspector] public Health health;
        [HideInInspector] public Ragdol ragdol;

        public AiAgentConfig config;

        [HideInInspector] public AiWeapons weapons;
        [HideInInspector] public Animator animator;
        [HideInInspector] public Animator rigController;
    
        [HideInInspector] public AiVisonSensor visonSensor;
        [HideInInspector] public AiTargetingSystem targetingSystem;

        [SerializeField] AiStateId currentState;
        #endregion
        
        
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            weapons = GetComponent<AiWeapons>();
            ragdol = GetComponent<Ragdol>();
            health = GetComponent<Health>();
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            Rigidbody rb = GetComponent<Rigidbody>();
            SphereCollider collider = GetComponent<SphereCollider>();
            animator = GetComponent<Animator>();
            rigController = GetComponentInChildren<Animator>();
            visonSensor = GetComponent<AiVisonSensor>();
            targetingSystem = GetComponent<AiTargetingSystem>();

            PrintErrorMessage();
            
            if(rb != null)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }

            if(collider != null)
            {
                collider.isTrigger = true;
            }


            stateMachine = new AiStateMachine(this);

            stateMachine.RegisterState(new AiChaseTargetState());
            stateMachine.RegisterState(new AiDeathState());
            stateMachine.RegisterState(new AiIdleState());
            stateMachine.RegisterState(new AiFindWeaponState());
            stateMachine.RegisterState(new AiAttackTargetState());
            stateMachine.RegisterState(new AiPatrolState());
            stateMachine.RegisterState(new AiFindTargetState());
            stateMachine.RegisterState(new AiSniperState());
            stateMachine.RegisterState(new AiFleeState());

            stateMachine.ChangeState(initialState);
            currentState = initialState;
        }

    
        void Update()
        {
            stateMachine.Update();
            currentState = stateMachine.currentState;
        }

        private void PrintErrorMessage()
        {
            if(navMeshAgent == null)
            {
                Debug.LogError("NavMeshAgent is null");
            }

            
            if(weapons == null)
            {
                Debug.LogError("AiWeapons is null");
            }

            
            if(ragdol == null)
            {
                Debug.LogError("Ragdol is null");
            }

            
            if(health == null)
            {
                Debug.LogError("AiAgentHealth is null");
            }

            
            if(playerTransform == null)
            {
                Debug.LogError("No gameObject found with tag Player");
            }

            
            if(GetComponent<Rigidbody>() == null)
            {
                Debug.LogError("No Rigidbody is attached with AiAgent GameObject ");
            }
            
            
            
            if(GetComponent<Collider>() == null)
            {
                Debug.LogError("SphereCollider is null");
            }
            
            

            if(playerTransform == null)
            {
                Debug.LogError("Player not found");
            }

            if(config == null)
            {
                Debug.LogError("AiAgentConfig is null");
            }

            
            if(animator == null)
            {
                Debug.LogError("No Animator found!");
            }

            
            if(rigController == null)
            {
                Debug.LogError("No Rig Controller animator found!");
            }
            
            
            if(visonSensor == null)
            {
                Debug.LogError("No VisionSensor found!");
            }

            
            if(targetingSystem == null)
            {
                Debug.LogError("No TargetingSystem found!");
            }

        }
    }
}