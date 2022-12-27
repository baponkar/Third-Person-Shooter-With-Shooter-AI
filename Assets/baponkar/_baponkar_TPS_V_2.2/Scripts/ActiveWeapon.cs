using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
//using UnityEditor.Animations;
using UnityEngine.UI;

public class ActiveWeapon : MonoBehaviour
{
    public enum WeaponSlot {
        primary = 0,
        secondary = 1,
        sniper = 2
    }

    #region Variables

    public WeaponWidget weaponWidget;
    public CharacterAiming characterAiming;
    public Animator rigController;
    Camera cam;
    //To recording weaponGrip position into the weapon animation clip.
    public Transform weaponLeftGrip; 
    public Transform weaponRightGrip;
    RaycastWeapon [] equiped_weapon = new RaycastWeapon[3];
    int activeWeaponIndex = 0;
    public Transform crossHairTarget;
    [Tooltip("Weapon Slot where the weapon is equipped")]
    public Transform [] weaponSlots;
    //for different weapon using different layer
    public Animator anim;
    //RigController Animator should be Update Mode - Animate Physics  and Culling Mode -Alawyas Animate
    //This bool helps not to fire during  holster weapon
    public bool isHolstered = false;
    
    #endregion

    void Start()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main;
        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();
        if(existingWeapon)
        {
            Equip(existingWeapon);
        }
    }

    
    void Update()
    {
        //Updating  crossHairTarget position  to the child of main camera.
        crossHairTarget = cam.transform.GetChild(0).transform.GetChild(1); 
        var weapon = GetWeapon(activeWeaponIndex); //currently activeWeaponIndex = 0
        
        if(weapon & !isHolstered)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                weapon.StartFireing();
            }
            if(weapon.isFireing)
            {
                weapon.UpdateFireing(Time.deltaTime);
            }
            weapon.UpdateWeapon(Time.deltaTime);

            if(Input.GetButtonUp("Fire1"))
            {
                weapon.StopFireing();
            }
        }
        


        if(Input.GetKeyDown(KeyCode.X))
        {
            ToggleActivateWeapon();
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveWeapon(WeaponSlot.primary);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveWeapon(WeaponSlot.secondary);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetActiveWeapon(WeaponSlot.sniper);
        }
    }

   

    public bool isFireing()
    {
        RaycastWeapon currentWeapon = GetActiveWeapon();
        if(!currentWeapon)
        {
            return false;
        }
        return currentWeapon.isFireing;
    }

    RaycastWeapon GetWeapon(int index)
    {
        if(index < 0 || index >= equiped_weapon.Length)
        {
            return null;
        }
        
        return equiped_weapon[index];
    }

    public void Equip(RaycastWeapon newWeapon)
    {
        int weaponSlotIndex = (int) newWeapon.weaponSlot;
        var weapon = GetWeapon(weaponSlotIndex);
        
        if(weapon)
        {
            Destroy(weapon.gameObject); //Destroying current weapon before equip a new one.
        }
        weapon = newWeapon;
        //make position of instantiate weapon inside of weaponParent
        weapon.transform.SetParent(weaponSlots[weaponSlotIndex], false);
        //As raycastDestination property is lost after making weapon prefab
        weapon.rigController = rigController;
        weapon.raycastDestination = crossHairTarget; 
        weapon.recoil.characterAiming = characterAiming;
        weapon.recoil.rigController = rigController;

        equiped_weapon[weaponSlotIndex] = weapon;
        SetActiveWeapon(newWeapon.weaponSlot);
        weaponWidget.Refresh(weapon.ammoCount);
    }

    void SetActiveWeapon(WeaponSlot weaponSlot)
    {
        int holsterIndex = activeWeaponIndex;
        int activateIndex =(int) weaponSlot;

        if(holsterIndex == activateIndex)
        {
            holsterIndex = -1;
        }

        StartCoroutine(SwitchWeapon(holsterIndex, activateIndex));
    }

    public RaycastWeapon GetActiveWeapon()
    {
        return GetWeapon(activeWeaponIndex);
    }

    IEnumerator SwitchWeapon(int holsterIndex, int activateIndex)
    {
       rigController.SetInteger("weaponIndex", activateIndex + 1);
       yield return StartCoroutine(HolsterWeapon(holsterIndex)); 
       yield return StartCoroutine(ActivateWeapon(activateIndex));
       activeWeaponIndex = activateIndex;
    }

    IEnumerator HolsterWeapon(int index)
    {
        isHolstered = true;
        var weapon = GetWeapon(index);
        if(weapon)
        {
            rigController.SetBool("holster_weapon", true);
            do 
            {
                yield return new WaitForEndOfFrame();
            } 
            while(rigController.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f);
        }
    }

    IEnumerator ActivateWeapon(int index)
    {
        isHolstered = false;
        var weapon = GetWeapon(index);

        if(weapon)
        {
            rigController.SetBool("holster_weapon", false);
            rigController.Play("equip_" + weapon.weaponName);
            do 
            {
                yield return new WaitForEndOfFrame();
            } 
            while(rigController.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f);
        }
    }

    public void ToggleActivateWeapon()
    {
        bool isholstered = rigController.GetBool("holster_weapon");
        if(isholstered)
        {
            StartCoroutine(ActivateWeapon(activeWeaponIndex));
        }
        else
        {
            StartCoroutine(HolsterWeapon(activeWeaponIndex));
        }
    }

    
    #region SetAnimation
    // [ContextMenu("Record And Save Weapon Grips")]
    // void SaveWeaponPose()
    // {
    //     GameObjectRecorder recorder = new GameObjectRecorder(gameObject);
    //     recorder.BindComponentsOfType<Transform>(weaponParent.gameObject, false);
    //     recorder.BindComponentsOfType<Transform>(weaponLeftGrip.gameObject, false);
    //     recorder.BindComponentsOfType<Transform>(weaponRightGrip.gameObject, false);
    //     recorder.TakeSnapshot(0.0f);

    //     recorder.SaveToClip(weapon.weaponAnimation);
    //     UnityEditor.AssetDatabase.SaveAssets();
    // }
    #endregion
}
