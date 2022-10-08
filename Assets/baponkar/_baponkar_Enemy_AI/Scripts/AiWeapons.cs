using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ThirdPersonShooter.Ai
{
    public class AiWeapons : MonoBehaviour
    {
        public enum WeaponSlot
        {
            primary = 0,
            secondary = 1,
            sniper = 2
        }

        #region Public Variables

        [Tooltip("Weapon Slot where the weapon is equipped")]
        public Transform [] weaponSlots;
        public Animator rigController;
        //This bool helps not to fire during  holster weapon
        public bool isHolstered = false;
        public Transform currentTarget;
        [HideInInspector] public Transform playerTransform;
        public Vector3 targetOffsetPosition;

        #endregion

        #region Private Variables

        RaycastWeapon [] equiped_weapon = new RaycastWeapon[3];
        int activeWeaponIndex = -1;
        AiAgent agent;
        bool weaponDroped = false;

        #endregion

        void Start()
        {
            agent = GetComponent<AiAgent>();
            playerTransform = agent.playerTransform;
            RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();
            if(existingWeapon)
            {
                Equip(existingWeapon);
            }
        }

        void Update()
        {
            var weapon = GetWeapon(activeWeaponIndex);
            if(weapon && agent.targetingSystem.HasTarget)
            {
                currentTarget.position = agent.targetingSystem.TargetPosition + targetOffsetPosition;
                weapon.UpdateWeapon(Time.deltaTime);
            }
        }

        public void SetFireing(bool enabled)
        {
            var currentWeapon = GetActiveWeapon();
            if(currentWeapon )
            {
                if(enabled && !rigController.GetBool("holster_weapon"))
                {
                    currentWeapon.StartFireing();
                    if(currentWeapon.isFireing)
                    {
                        currentWeapon.UpdateFireing(Time.deltaTime);
                    }
                }
                else
                {
                    currentWeapon.StopFireing();
                }
            }
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

            //As raycastDestination and rigController property is lost after making weapon prefab
            weapon.rigController = rigController;
            weapon.raycastDestination = currentTarget;

            equiped_weapon[weaponSlotIndex] = weapon;
            SetActiveWeapon((WeaponSlot) newWeapon.weaponSlot);
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
            rigController.SetInteger("weaponIndex",activateIndex + 1);
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
                do 
                {
                    yield return new WaitForEndOfFrame();
                } 
                while(rigController.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f);
                rigController.SetBool("holster_weapon", true);
            }
        }

        IEnumerator ActivateWeapon(int index)
        {
            isHolstered = false;
            var weapon = GetWeapon(index);

            if(weapon)
            {
                rigController.Play("equip_" + weapon.weaponName);
                do 
                {
                    yield return new WaitForEndOfFrame();
                }

                while(rigController.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
                rigController.SetBool("holster_weapon", false);
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

        public void ActivateWeapon()
        {
            StartCoroutine(ActivateWeapon(activeWeaponIndex));
        }

        public void DeActivateWeapon()
        {
            StartCoroutine(HolsterWeapon(activeWeaponIndex));
        }

        public bool HasWeapon()
        {
            RaycastWeapon currentWeapon = GetActiveWeapon();
            return currentWeapon != null;
        }

        public void OnAnimationEvent(string eventName)
        {
            if(eventName == "equipedWeapon")
            {
                RaycastWeapon currentWeapon = GetActiveWeapon();
                currentWeapon.transform.SetParent(weaponSlots[(int)currentWeapon.weaponSlot], false);
            }
        }

        public void DropWeapon()
        {
            if(!weaponDroped)
            {
                for(int i=0;i<equiped_weapon.Length;i++)
                {
                    if(equiped_weapon[i] != null)
                    {
                        equiped_weapon[i].transform.SetParent(null);
                        equiped_weapon[i].GetComponent<BoxCollider>().enabled = true;
                        Rigidbody rb = equiped_weapon[i].gameObject.AddComponent<Rigidbody>();
                        rb.useGravity = true;
                        equiped_weapon[i] = null;
                        SetFireing(false);
                    }
                }
                weaponDroped = true;
            }   
        }
    }
}