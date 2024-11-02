using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.Serzz0.Ballkour
{
    public class ThirdPersonCam : MonoBehaviour
    {
        #region Object References
        /// <summary>
        /// References the needed components.
        /// </summary>
        [Header("References")]
        public Transform orientation;
        public Transform player;
        public Transform playerObj;
        public Rigidbody rb;

        public float rotationSpeed;
        #endregion

        private void Start ()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            // rotate orientation
            Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
            orientation.forward = viewDir.normalized;

            // rotate player object.
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero) 
            { 
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }
        }
    }
}
