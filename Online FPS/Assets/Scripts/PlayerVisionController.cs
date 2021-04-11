using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.DarkLynxDEV.OnlineFPS
{
    public class PlayerVisionController : MonoBehaviour
    {

        #region Variables

        public static bool cursorLocked = true;

        public Transform player;
        public Transform playerCam;
        public Transform weapon;

        public float xSensitivity;
        public float ySensitivity;
        public float yMinMax;

        private Quaternion camOrigin;

        #endregion

        #region MonoBehaviours Callbacks

        private void Start()
        {
            camOrigin = playerCam.localRotation;
        }

        private void Update()
        {
            SetY(); //Rotate the camera up and down - Clamped
            SetX(); //Rotate the player - Unclamped

            UpdateCursorLock();
        }

        #endregion

        #region Public Methods



        #endregion

        #region Private Methods

        private void SetY() {
            float mouseY = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
            Quaternion adj_Y = Quaternion.AngleAxis(mouseY, -Vector3.right);
            Quaternion delta_Y = playerCam.localRotation * adj_Y;

            if (Quaternion.Angle(camOrigin, delta_Y) < yMinMax) {
                playerCam.localRotation = delta_Y;
            }

            weapon.rotation = playerCam.rotation;
        }

        private void SetX() {
            float mouseX = Input.GetAxis("Mouse X") * ySensitivity * Time.deltaTime;
            Quaternion adj_X = Quaternion.AngleAxis(mouseX, Vector3.up);
            Quaternion delta_X = player.localRotation * adj_X;
            player.localRotation = delta_X;
        }

        private void UpdateCursorLock() {
            if (cursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                if (Input.GetKeyDown(KeyCode.Escape)) {
                    cursorLocked = false;
                }

            } else {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                if (Input.GetKeyDown(KeyCode.Escape)) {
                    cursorLocked = true;
                }

            }
        }

        #endregion
    }
}
