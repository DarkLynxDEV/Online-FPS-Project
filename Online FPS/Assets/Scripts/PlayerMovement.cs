using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.DarkLynxDEV.OnlineFPS
{
    public class PlayerMovement : MonoBehaviour
    {

        #region Variables

        [Header("Input Fields")]
        public float moveSpeed;
        public float jumpForce;
        public float sprintModifier;
        public Camera playerCamera;
        public Transform groundCheckOrigin;
        public LayerMask environment;

        private Rigidbody rb;
        private float baseFOV;
        private float sprintFOVModifier = 1.375f;
        private float sprintFOVSmoothing = 8f;
        private bool isGrounded;

        #endregion

        #region MonoBehaviour Callbacks

        private void Start() {
            baseFOV = playerCamera.fieldOfView;
            Camera.main.enabled = false; //Set the main camera to false - makes Player Camera is the only valid camera
            rb = GetComponent<Rigidbody>();
        }

        private void Update() {
            PlayerInput(); //Things that need to be immediate
        }

        private void FixedUpdate() {
            PlayerDiscreetInput(); //Things that can be skipped
        }


        #endregion

        #region Public Methods
        #endregion

        #region Private Methods

        private void PlayerDiscreetInput() {
            //Player Movement - General Movement
            float xInput = Input.GetAxisRaw("Horizontal");
            float zInput = Input.GetAxisRaw("Vertical");

            //Player Movement - Ground Raycast
            bool isGrounded = Physics.Raycast(groundCheckOrigin.position, Vector3.down, 0.1f, environment);

            //Player Movement - Jumping
            bool jump = Input.GetKeyDown(KeyCode.Space);
            bool isJumping = jump && isGrounded;

            if (isJumping) {
                rb.AddForce(Vector3.up * jumpForce);
            }

            //Player Movement - Sprinting
            bool sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            bool isSprinting = sprint && zInput > 0f && !isJumping && isGrounded;

            float speed = moveSpeed;
            if (isSprinting) {
                speed *= sprintModifier;
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, baseFOV * sprintFOVModifier, Time.deltaTime * sprintFOVSmoothing); //Zoom Out gradually for effect
            } else {
                playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, baseFOV, Time.deltaTime * sprintFOVSmoothing);
            }

            Vector3 direction = new Vector3(xInput, 0f, zInput);
            direction.Normalize();

            Vector3 targetVelocity = transform.TransformDirection(direction) * speed * Time.deltaTime;
            targetVelocity.y = rb.velocity.y;
            rb.velocity = targetVelocity;
        }

        private void PlayerInput() {
            //Player Movement - General Movement
            float xInput = Input.GetAxisRaw("Horizontal");
            float zInput = Input.GetAxisRaw("Vertical");

            //Player Movement - Ground Raycast
            bool isGrounded = Physics.Raycast(groundCheckOrigin.position, Vector3.down, 0.1f, environment);

            //Player Movement - Jumping
            bool jump = Input.GetKeyDown(KeyCode.Space);
            bool isJumping = jump && isGrounded;

            if (isJumping) {
                rb.AddForce(Vector3.up * jumpForce);
            }

            //Player Movement - Sprinting
            bool sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            bool isSprinting = sprint && zInput > 0f && !isJumping && isGrounded;
        }

        #endregion
    }
}
