using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRagdol : MonoBehaviour
{
    // Rigidbody [] rigidbodies;
    // CapsuleCollider [] capsuleColliders;

    // CharacterLocomotion characterLocomotion;
    // CharacterAiming characterAiming;
    // PlayerHealth playerHealth;

    // public bool isRagdoll = false;

    // Animator anim;

    // public GameObject deathVCam;


    // void Awake()
    // {
    //     deathVCam.SetActive(false);
    //     rigidbodies = GetComponentsInChildren<Rigidbody>();
    //     capsuleColliders = GetComponentsInChildren<CapsuleCollider>();
    //     anim = GetComponent<Animator>();

    //     foreach(Rigidbody rigidbody in rigidbodies)
    //     {
    //         rigidbody.isKinematic = true;
    //         rigidbody.detectCollisions = false;
    //     }

    //     foreach(CapsuleCollider capsuleCollider in capsuleColliders)
    //     {
    //         capsuleCollider.enabled = false;
    //     }

    //     characterLocomotion = GetComponent<CharacterLocomotion>();
    //     characterAiming = GetComponent<CharacterAiming>();
    //     playerHealth = GetComponent<PlayerHealth>();

    // }
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     if(playerHealth.isDead)
    //     {
    //         isRagdoll = true;
    //         Death();
    //     }
    // }

    // public void Death()
    // {
    //     deathVCam.SetActive(true);
    //     characterLocomotion.enabled = false;
    //     characterAiming.enabled = false;
    //     anim.enabled = false;

    //     foreach(Rigidbody rigidbody in rigidbodies)
    //     {
    //         rigidbody.isKinematic = false;
    //         rigidbody.detectCollisions = true;
    //     }
    //     foreach(CapsuleCollider capsuleCollider in capsuleColliders)
    //     {
    //         capsuleCollider.enabled = true;
    //     }
    // }
}
