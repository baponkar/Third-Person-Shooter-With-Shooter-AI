using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Animations.Rigging;

public class PlayerDeathEffect : MonoBehaviour
{

    

    Animator anim;
    CharacterLocomotion characterLocomotion;
    CharacterAiming characterAiming;


    public Rig [] rigs;

    public ActiveWeapon activeWeapon;

    public GameObject[] ui;

    public GameObject cameraGroup;

    public Animator hitAnimator;
    Health health;

    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        anim = GetComponent<Animator>();
        characterLocomotion = GetComponent<CharacterLocomotion>();
        characterAiming = GetComponent<CharacterAiming>();
        activeWeapon = GetComponent<ActiveWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    public void PlayerDead()
    {
        hitAnimator.Play("Death Animation");
        cameraGroup.SetActive(true);
        
        
        characterLocomotion.enabled = false;
        characterAiming.enabled = false;
        activeWeapon.enabled = false;
        foreach(Rig rig in rigs)
        {
            rig.weight = 0f;
        }
        anim.SetTrigger("isDead");
    }
}
