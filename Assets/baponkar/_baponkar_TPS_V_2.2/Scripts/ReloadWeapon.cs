using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadWeapon : MonoBehaviour
{
    public Animator rigController;
    public WeaponAnimationEvent animationEvents;
    public WeaponWidget weaponWidget;
    public ActiveWeapon activeWeapon;
    public Transform leftHand;

    GameObject magazineHand;
    RaycastWeapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        animationEvents.weaponAnimationEvent.AddListener(OnAnimationEvent);
    }

    // Update is called once per frame
    void Update()
    {
        weapon = activeWeapon.GetActiveWeapon();
        if(weapon)
        {
            if(Input.GetKeyDown(KeyCode.R) || weapon.ammoCount <= 0)
            {
                rigController.SetTrigger("reload_weapon");
            }

            if(weapon.isFireing)
            {
                weaponWidget.Refresh(weapon.ammoCount);
            }
        }
    }

    void OnAnimationEvent(string eventName)
    {
       //Debug.Log("Event Name: " + eventName);
       switch(eventName)
       {
           case "detaching_magazine":
               DetachWeapon();
               break;
            case "droping_magazine":
                DropWeapon();
                break;
            case "refilling_magazine":
                RefillWeapon();
                break;
            case "attaching_magazine":
                AttachWeapon();
                break;
            
       }
    }

    void DetachWeapon()
    {
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        magazineHand = Instantiate(weapon.magazine, leftHand, true);
        weapon.magazine.SetActive(false);
        if(weapon.reloadClip)
        {
            weapon.audioSource.PlayOneShot(weapon.reloadClip);
        }
        
    }
    void DropWeapon()
    {
       GameObject dropmagazine = Instantiate(magazineHand, magazineHand.transform.position, magazineHand.transform.rotation);
       dropmagazine.AddComponent<Rigidbody>();
       dropmagazine.AddComponent<BoxCollider>();
       magazineHand.SetActive(false);
       SelfDestruct selfDestruct = dropmagazine.AddComponent<SelfDestruct>();
       selfDestruct.lifeTime = 5.0f;
    }
    
    void RefillWeapon()
    {
        magazineHand.SetActive(true);
        
    }
   
    void AttachWeapon()
    {
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        if(weapon.weaponName == "pistol1" || weapon.weaponName == "assult1")
        {
            weapon.audioSource.PlayOneShot(weapon.reloadClip);
        }
        weapon.magazine.SetActive(true);
        Destroy(magazineHand);

        weapon.ammoCount = weapon.clipSize;
        rigController.ResetTrigger("reload_weapon");
        weaponWidget.Refresh(weapon.ammoCount);
    }

    
}
