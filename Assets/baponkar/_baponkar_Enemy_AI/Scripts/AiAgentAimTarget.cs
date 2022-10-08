using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ThirdPersonShooter.Ai
{

    public class AiAgentAimTarget : MonoBehaviour
    {
        #region Variables
        public bool isAiming = false;
        public Transform playerTransform;
        public Transform aiAimTarget;
        private Vector3 initialOffset = new Vector3(0f,1.78f, 5f);
        public Vector3 offset;
        private float turnSpeed = 720f;
        Transform raycastOrigin;
        AiWeapons aiWeapons;
        RaycastWeapon weapon;
        #endregion

        void Start()
        {
            aiWeapons = GetComponent<AiWeapons>(); 
        }

        
        void FixedUpdate()
        {

            weapon = aiWeapons.GetActiveWeapon();
            raycastOrigin = weapon.raycastOrigin;
            offset.y = Random.Range(0f,1.78f);
            offset.x = Random.Range(-.5f,.5f);
            if(isAiming)
            {
                aiAimTarget.position = playerTransform.position;
                FaceTowardsPlayer();
            }
            
        }

        void FaceTowardsPlayer()
        {   
            Vector3 direction = (playerTransform.position - raycastOrigin.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3 (direction.x,0,direction.z));
            aiAimTarget.transform.rotation = Quaternion.Lerp(aiAimTarget.transform.rotation, lookRotation,Time.time * turnSpeed);
        }

        void OnDrawGizmos()
        {
            if(isAiming)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(raycastOrigin.position, aiAimTarget.position);
            }
        }
    }
}
