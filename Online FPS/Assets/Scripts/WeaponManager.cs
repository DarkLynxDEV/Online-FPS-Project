using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.DarkLynxDEV.OnlineFPS
{
    public class WeaponManager : MonoBehaviour
    {

        #region Variables

        public Gun[] loadout;
        public Transform weaponParent;

        private GameObject currentWeapon;

        #endregion

        #region MonoBehaviour Callbacks

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                Equip(0);
            }
        }

        #endregion

        #region Public Methods



        #endregion

        #region Private Methods

        private void Equip(int weapon_ind) {

            if (currentWeapon != null) {
                Destroy(currentWeapon);
            }

            GameObject newEquipment = Instantiate(loadout[weapon_ind].weaponPrefab, weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
            newEquipment.transform.localPosition = Vector3.zero;
            newEquipment.transform.localEulerAngles = Vector3.zero;

            currentWeapon = newEquipment;
        }

        #endregion
    }
}
