using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.DarkLynxDEV.OnlineFPS
{
    [CreateAssetMenu(fileName = "New Gun Stats", menuName = "Gun Stats")]
    public class Gun : ScriptableObject
    {

        #region Variables

        [Header("Required Gun Input Fields")]
        public string name;
        public GameObject weaponPrefab;

        [Header("General Gun Input Fields")]
        public float fireRate;

        #endregion

        #region Public Methods
        #endregion
    }
}
