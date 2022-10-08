using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirdPersonShooter.Ai;


public class WeaponPickup : MonoBehaviour
{
    public RaycastWeapon weaponPrefab;
    

    public void OnTriggerEnter(Collider other)
    {
        //When player collide with weapon pickup, it will spawn the weapon prefab
        ActiveWeapon activeWeapon = other.gameObject.GetComponent<ActiveWeapon>();
        if(activeWeapon != null)
        {
            RaycastWeapon newWeapon = Instantiate(weaponPrefab);
            activeWeapon.Equip(newWeapon);
            Destroy(gameObject);
        }

        // HitBox hitBox = other.gameObject.GetComponent<HitBox>();
        // if(hitBox)
        // {
        //     AiWeapons  weapons  = hitBox.health.GetComponent<AiWeapons>();
        //     if(weapons != null)
        //     {
        //         RaycastWeapon newWeapon = Instantiate(weaponPrefab);
        //         weapons.Equip(weaponPrefab);
        //         Destroy(gameObject);
        //     }
        // }

        //when ai collide with weapon pickup, it will spawn the weapon prefab  
        AiWeapons aiWeapons = other.gameObject.GetComponent<AiWeapons>();
        if(aiWeapons != null)
        {
            RaycastWeapon newWeapon = Instantiate(weaponPrefab);
            aiWeapons.Equip(newWeapon);
            Destroy(gameObject);
        }
    }
}
