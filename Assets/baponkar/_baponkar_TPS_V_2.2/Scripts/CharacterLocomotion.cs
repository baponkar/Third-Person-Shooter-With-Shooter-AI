//Writer : Bapon Kar
//Build Date : 28/05/2022
//Last Update : 10/10/2022

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class CharacterLocomotion : MonoBehaviour
    {
        [Header("Player Movement Control ")]
        
        #region Public Variables
        public Animator rigController;
        public float jumpHeight = 3f;
        public float gravity = 20f;
        public float stepDown = 0.3f;
        public float airControl = 2.5f;
        public float jumpDamp = 0.5f;
        public float groundSpeed = 1.5f;
        public float pushPower = 2.0f;
        [HideInInspector] public CharacterController cc;
        #endregion

        #region Private Variables
        ActiveWeapon activeWeapon;
        Animator animator;
        Vector2 input;
        Vector3 rootMotion;
        Vector3 velocity;
        bool jumpInput;
        bool isJumping;
        bool sprintInput;
        bool isCrouching;
        int isSprintingParam = Animator.StringToHash("isSprinting");
        CharacterAiming characterAiming;
        bool isAiming;
        #endregion

        #region MonoBehaviorMethods
        void Start()
        {
            animator = GetComponent<Animator>();
            cc = GetComponent<CharacterController>();
            activeWeapon = GetComponent<ActiveWeapon>();
            characterAiming = GetComponent<CharacterAiming>();
        }

        void Update()
        {
            #if UNITY_EDITOR
                //Debug.Log("This is Unity Editor");
                ProcesInputs();
            #elif UNITY_ANDROID
                //Debug.Log("This is Android platform");
            #elif UNITY_IOS
                //Debug.Log("This is IOS Platform");
            #elif UNITY_STANDALONE_WIN
                //Debug.Log("This is Windows platform");
                ProcesInputs();
            #elif UNITY_STANDALONE_OSX
                ProcesInputs();
            #elif UNITY_WEBGL
                //Debug.Log("This is WebGL Platform");
                ProcesInputs();
            #endif

            animator.SetFloat("inputX", input.x);
            animator.SetFloat("inputY", input.y);

            UpdateInSprinting();

            if(jumpInput)
            {
                Jump();
            }

            Crouching();
            
        }

        void FixedUpdate()
        {
            if(isJumping)
            {
                //IsInAirState
                UpdateInAir();
            }
            else
            {
                //Ground state
                UpdateInGround();
            }
            
        }
        #endregion

        #region  Methods

        void ProcesInputs()
        {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");
            jumpInput = Input.GetKeyDown(KeyCode.Space);
            sprintInput = Input.GetKey(KeyCode.LeftShift);
            

            
            if(Input.GetKeyDown(KeyCode.C))
            {
                isCrouching = !isCrouching;
            }
        }

        public void OnAnimatorMove()
        {

            rootMotion = animator.deltaPosition;
        }

        bool IsSprinting()
        {
            bool isSprinting = sprintInput;
            bool isFireing = activeWeapon.isFireing();
            isAiming = characterAiming.isAiming;
            
            return isSprinting && !isFireing && !isAiming;
        }

        void Crouching()
        {
            if(isCrouching)
            {
                animator.SetBool("isCrouching", true);
                //cc.height = 0.94f;
                //cc.center = new Vector3(0,0.93f,0);
                
            }
            else
            {
                animator.SetBool("isCrouching", false);
                //cc.height = 1.6f;
                //cc.center = new Vector3(0,0.9f,0);
            }
        }


        void UpdateInSprinting()
        {
            bool isSprinting = IsSprinting();
            
            animator.SetBool(isSprintingParam, isSprinting);
            rigController.SetBool(isSprintingParam, isSprinting);
        }

        Vector3 CalculateAirControl()
        {
            return (transform.right * input.x + transform.forward * input.y) * (airControl/100);
        }

        public void Jump()
        {
            if(!isJumping)
            {
                float jumpVelocity = Mathf.Sqrt(2 * jumpHeight * gravity);
                SetInAir(jumpVelocity);
            }
        }

        void SetInAir(float jumpVelocity)
        {
            isJumping = true;
            velocity = animator.velocity * jumpDamp * groundSpeed;
            velocity.y = jumpVelocity;
            animator.SetBool("isJumping", true);
        }

        void UpdateInAir()
        {
            velocity.y -= gravity * Time.fixedDeltaTime;
            Vector3 displacement = velocity * Time.fixedDeltaTime;
            displacement += CalculateAirControl();
            cc.Move(displacement);
            isJumping = !cc.isGrounded;
            rootMotion = Vector3.zero;
            animator.SetBool("isJumping", isJumping);
        }

        void UpdateInGround()
        {
            Vector3 stepDownForwardAmount = rootMotion * groundSpeed;
            Vector3 stepDownAmount = Vector3.down * stepDown;
            cc.Move(stepDownForwardAmount + stepDownAmount);
            rootMotion = Vector3.zero; 

            if(!cc.isGrounded)
            {
                SetInAir(0);
            }
        }

        void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;

            // no rigidbody
            if (body == null || body.isKinematic)
                return;

            // We dont want to push objects below us
            if (hit.moveDirection.y < -0.3f)
                return;

            // Calculate push direction from move direction,
            // we only push objects to the sides never up and down
            Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

            // If you know how fast your character is trying to move,
            // then you can also multiply the push velocity by that.

            // Apply the push
            body.velocity = pushDir * pushPower;
        }
        #endregion
    }

