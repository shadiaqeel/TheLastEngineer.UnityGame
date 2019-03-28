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

        
        public Item currentWeapon;
        public Weapon firstWeapon;
        public Weapon secondWeapon; 
        public Tool tool;

         Animator anim;

         private bool catchWeaponEvent = false;

        
        #endregion Settings





        #region Initialization

        
        /// Awake is called when the script instance is being loaded.
       
        void Awake()
        {
            anim = GetComponent<Animator>();
            anim.avatar = GetComponentsInChildren<Animator>()[1].avatar;    
        }

        // Update is called once per frame
        void Update()
        {

                if(currentWeapon !=null)
                {   
                    
                    anim.SetInteger("WeaponID",currentWeapon.ID);
          
                }else{
                 
                    anim.SetInteger("WeaponID",0);
            

                }

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


                    if(firstWeapon==null)
                    {
                        ((Weapon)item).orderOfWeapon =1;
                        firstWeapon =(Weapon)item;
                    }else{
                        ((Weapon)item).orderOfWeapon =2;
                        secondWeapon =(Weapon)item;

                    }
                    ((Weapon)item).Unequip();
                    item.transform.SetParent (positionSettings.Weapons);
                    
                    break;
                    

                }
     



        }

        #endregion PickUp




        #region Fire

        public void Fire (bool state)
        {
            anim.SetBool("Fire",state);

        }

        #endregion Fire




        #region SwitchWeapons

        public void SwitchWeapons (int weaponNum){
            

            if(currentWeapon!=null)
                {anim.SetInteger("ItemLocation",0);
                    //(currentWeapon).Unequip();
                    }

            switch(weaponNum)
            {
                case 1:

                if(currentWeapon!=firstWeapon)
                {anim.SetInteger("ItemLocation",1);              
                //firstWeapon.Equip();
                currentWeapon = firstWeapon;}
                else {currentWeapon = null;}

                break;

                case 2:
                if(currentWeapon!=secondWeapon)
                {anim.SetInteger("ItemLocation",2);
                //secondWeapon.Equip();
                currentWeapon = secondWeapon;}
                else {currentWeapon = null;}
                break;

            }
        
            
        }


        //Animation Event
        public void EquipUnEquipWeapon(int OpID )
        {


                switch(OpID)
                {

                    case 10:
                        firstWeapon.Unequip();
                    break;

                    case 20:
                        secondWeapon.Unequip();
                    break;

                    case 11:
                        firstWeapon.Equip();
                    break;


                    case 21:
                        secondWeapon.Equip();
                    break;
                
                    default:
                    Debug.Log("OpID does not Valid");
                    return;


                } 
            

        }




        public void Drop ()
        {
                
                        
            

            anim.SetInteger("ItemLocation",0);
            anim.SetTrigger("DropItem");


            if(currentWeapon==firstWeapon)
                firstWeapon=null;
            if(currentWeapon==secondWeapon)
                secondWeapon=null;

            currentWeapon.Drop();
            currentWeapon.gameObject.tag = "Item"; 
            currentWeapon.gameObject.layer=9;
            currentWeapon = null;

            




        }





        #endregion SwitchWeapons



      /*  
void OnAnimatorIK()
    {
        if (!anim)
            return;

        if (currentWeapon && positionSettings.leftHand )
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            Transform target = currentWeapon.userSettings.leftHandIKTarget;
            Vector3 targetPos = target.position;
            Quaternion targetRot = target.rotation;
            anim.SetIKPosition(AvatarIKGoal.LeftHand, targetPos);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, targetRot);
        }
        else
        {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
        }
    }
*/ 
        





    }



}
