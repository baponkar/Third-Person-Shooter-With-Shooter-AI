using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Baponkar.FPS
{
    [RequireComponent(typeof(Camera))]
    [RequireComponent(typeof(AudioListener))]
    public class MouseLook : MonoBehaviour
    {
        #region Variables
        float mouseSensitivity= 100f;
        float xRotation;
        float yRotation;
        public Transform playerBody;
        #endregion

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            yRotation += mouseX;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            //yRotation = Mathf.Clamp(yRotation, 0.0f, 360f);
            transform.localRotation = Quaternion.Euler(xRotation, 0, 0f);
            playerBody.Rotate(Vector3.up * mouseX);

        }
    }
}
