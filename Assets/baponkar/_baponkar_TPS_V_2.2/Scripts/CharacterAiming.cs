using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAiming : MonoBehaviour
{
    public Transform cameraLookAt;
    Camera mainCamera;
    [Tooltip("This is the speed the player rotate towards camera")]public float turnSpeed = 15f;
    //[Tooltip("This is the time taken to aim")] float aimDuration = 0.1f;

    [Tooltip("Find the raycastweapon in the children")] RaycastWeapon weapon;

    //using Virtual camera instead of free look camera
    //I need to add some mouse control state of cinemachine into virtual camera as it is not have
    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;

    public bool isAiming = false;

    public Animator anim;
    ActiveWeapon activeWeapon;

    int isAimingParam = Animator.StringToHash("isAiming");

    public float cameraSensitivity = 1.0f;
    
    
    void Start()
    {
        mainCamera = Camera.main;
        //making cursor invisible
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //anim = GetComponent<Animator>();
        activeWeapon = GetComponent<ActiveWeapon>();

    }

    void Update()
    {
        isAiming = Input.GetButton("Fire2");

        anim.SetBool(isAimingParam, isAiming);

        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();

        if(weapon != null)
        {
            weapon.recoil.recoilModifier = isAiming ? 0.3f : 1f;
        }
       
    }

    
    void FixedUpdate()
    {
        xAxis.Update(Time.fixedDeltaTime);
        yAxis.Update(Time.fixedDeltaTime);

        cameraLookAt.eulerAngles = new Vector3(cameraSensitivity * yAxis.Value, cameraSensitivity * xAxis.Value, 0);
       //rotating player towards camera 
       float yawCamera = mainCamera.transform.eulerAngles.y;
       transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, yawCamera, 0f), turnSpeed * Time.fixedDeltaTime);
    }
}
