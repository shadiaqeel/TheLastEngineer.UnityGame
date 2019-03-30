using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using ThelastEngineering.Inventory;


namespace ThelastEngineering.PlayerGroup
{    

    public class Weaponhandler : PlayerManager
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

         

         Item pickUpItem;

         private bool catchWeaponEvent = false;

        
        #endregion Settings





        #region Initialization

        
       


        // Update is called once per frame
        void Update()
        {

                if(currentWeapon !=null)
                {   
                    
                    player._anim.SetInteger("WeaponID",currentWeapon.ID);
          
                }else{
                 
                    player._anim.SetInteger("WeaponID",0);
            

                }

        }

        #endregion Initialization




    [SerializeField] FullBodyBipedEffector[] effectors;
    private InteractionSystem interactionSystem;
    [SerializeField] InteractionObject interactionObject;




        #region PickUP
        public void PickUp (Item item)
        {  
            /*    
            interactionSystem = GetComponent<InteractionSystem>();
                pickUpItem = item;

                	foreach (FullBodyBipedEffector e in effectors) {
					interactionSystem.StartInteraction(e, interactionObject, true);
				}
                
*/  

                             //--------------------------------------------------------
                             
                 
                player._anim.SetTrigger ("PickUp");
                
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
            player._anim.SetBool("Fire",state);

        }

        #endregion Fire




        #region SwitchWeapons

        public void SwitchWeapons (int weaponNum){
            

            if(currentWeapon!=null)
                {player._anim.SetInteger("ItemLocation",0);
                    //(currentWeapon).Unequip();
                    }

            switch(weaponNum)
            {
                case 1:

                if(currentWeapon!=firstWeapon)
                {player._anim.SetInteger("ItemLocation",1);              
                //firstWeapon.Equip();
                currentWeapon = firstWeapon;}
                else {currentWeapon = null;}

                break;

                case 2:
                if(currentWeapon!=secondWeapon)
                {player._anim.SetInteger("ItemLocation",2);
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
                
                        
            

            player._anim.SetInteger("ItemLocation",0);
            player._anim.SetTrigger("DropItem");


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



      
void OnAnimatorIK()
    {
        if (!player._anim)
            return;
    /* 
        if(anim.GetCurrentAnimatorStateInfo(2).IsTag("PickUp") && pickUpItem!=null)
        {   
            Debug.Log(2222222222);
            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);

            anim.SetIKPosition(AvatarIKGoal.LeftHand, pickUpItem.transform.position);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, pickUpItem.transform.localRotation);
        }
    */

/*  
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


        */ 
    }

        





    }



}
