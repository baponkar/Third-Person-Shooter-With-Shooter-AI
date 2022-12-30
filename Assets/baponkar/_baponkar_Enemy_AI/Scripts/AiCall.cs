using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ThirdPersonShooter.Ai
{
    public class AiCall : MonoBehaviour
    {
        Health health;

        AiTargetingSystem targetingSystem;
        AiAgent agent;

        public bool alert;
        public float range = 5f;

        void Start()
        {
            health = GetComponent<Health>();
            agent = GetComponent<AiAgent>();
        }

        
        void Update()
        {
            if (targetingSystem.HasTarget)
            {
                alert = true;
            }
            else
            {
                alert = false;
            }
        }

        void Call()
        {
            if(alert)
            {
                Vector3 pos = FindNearAgent().position;
                agent.FaceTowardTarget(pos);
            }
        }

        Transform FindNearAgent()
        {
            Transform nearObj = null;
            float minDis = Mathf.Infinity;
            var objs = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(var obj in objs)
            {
                float distance = Vector3.Distance(transform.position, obj.transform.position);
                if(distance < minDis)
                {
                    minDis = distance;
                    nearObj = obj.transform;
                }
            }

            return nearObj;
        }
    }
}
