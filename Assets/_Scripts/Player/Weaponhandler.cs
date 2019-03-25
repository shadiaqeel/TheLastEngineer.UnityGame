using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThelastEngineering.Inventory;


namespace ThelastEngineering.Player
{    

    public class Weaponhandler : MonoBehaviour
    {

        #region Settings

        [System.Serializable]
        public class PositionSettings
        {
            public Transform rightHand;
            public Transform leftHand;

            public Transform UnequipSpot;
            public Transform Backpack;
            public Transform Weapons;
            public Transform FirstUnequipWeaponSpot;
            public Transform SecondUnequipWeaponSpot;

            public Transform ToolUnequipWeaponSpot;

            
        }
        [SerializeField]
        public PositionSettings positionSettings;


        private Item currentWeapon;


        public int currentWeaponindex;


        #endregion Settings


        #region Initialization

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        #endregion Initialization


        #region PickUP
        public void PickUp (Item item)
        {

            item.owner = this;
            item.Unequip();

        }

        #endregion PickUp



        #region SwitchWeapons

        public void SwitchWeapons (){}


        #endregion SwitchWeapons

        
    }
}
