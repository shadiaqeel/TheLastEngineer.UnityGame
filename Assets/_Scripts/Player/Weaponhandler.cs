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
        private Weapon firstWeapon;
        private Weapon secondWeapon; 
        private Tool tool;

        
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

                //to reduce load on vision sensor
                item.gameObject.tag = "taked"; 
                item.gameObject.layer=0;

                switch (item.Type)
                {
                    case Item.ItemType.Element:
                    ((Element)item).Unequip();
                    item.transform.SetParent (positionSettings.Backpack);
                    break;

                    case Item.ItemType.Tool:
                    ((Tool)item).Unequip();
                    tool = (Tool)item;
                    item.transform.SetParent (positionSettings.Weapons);

                    break;

                    case Item.ItemType.Weapon:
                    ((Weapon)item).Unequip();
                    item.transform.SetParent (positionSettings.Weapons);

                    if(firstWeapon==null)
                    {
                        ((Weapon)item).orderOfWeapon =1;
                    }else{
                        ((Weapon)item).orderOfWeapon =2;
                    }
                    
                    break;
                    

                }
     



        }

        #endregion PickUp



        #region SwitchWeapons

        public void SwitchWeapons (){


        
            
        }


        #endregion SwitchWeapons

        
    }
}
