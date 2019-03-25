using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using ThelastEngineering.Player;




namespace ThelastEngineering.Inventory
{
    [RequireComponent(typeof(ParentConstraint))]
    [RequireComponent(typeof(SphereCollider))]
    public class Item : MonoBehaviour
    {

        public enum ItemType
        {Item,Tool,Weapon}

        [System.Serializable]
        public class PlayerSettings
        {



            [Header("Positioning")]
            public Vector3 equipPosition;
            public Vector3 equipRotation;
            public Vector3 unequipPosition;
            public Vector3 unequipRotation;

        }

        
        public PlayerSettings playerSettings;

        internal ParentConstraint parentConstraint;



         [SerializeField]internal  ItemType Type;
        [SerializeField]internal int ID;
        [SerializeField]internal string name;
     
        [SerializeField]internal string description;
        [SerializeField]internal Texture2D icon ;
        [SerializeField]internal  Weaponhandler owner;
        
    
        [SerializeField]internal SphereCollider col;

        [SerializeField]internal  bool equipped;


    
        protected void init()
        {

            parentConstraint= GetComponent<ParentConstraint>();
            col = GetComponent<SphereCollider>();
        }






       internal virtual void Equip()
       {
           if (!owner)
               return;
           else if (!owner.positionSettings.rightHand)
               return;
           //transform.SetParent(owner.positionSettings.rightHand);
           
           ConstraintSource cs = new ConstraintSource();
           cs.sourceTransform = owner.positionSettings.rightHand;
           cs.weight = 1;

            Debug.Log(parentConstraint==null);
           parentConstraint.AddSource(cs);
           parentConstraint.
           transform.localPosition = playerSettings.equipPosition;
           Quaternion equipRot = Quaternion.Euler(playerSettings.equipRotation);
           transform.localRotation = equipRot;
       }


       //Unequips the weapon and places it to the desired location
       internal virtual void  Unequip()
       {
           if (!owner)
               return;
           //transform.SetParent(owner.positionSettings.UnequipSpot);
           ConstraintSource cs = new ConstraintSource();
           cs.sourceTransform = owner.positionSettings.rightHand;
           cs.weight = 1;
        
           parentConstraint.SetSource(0,cs);
           transform.localPosition = playerSettings.unequipPosition;
           Quaternion unEquipRot = Quaternion.Euler(playerSettings.unequipRotation);
           transform.localRotation = unEquipRot;
       }





    }
}
