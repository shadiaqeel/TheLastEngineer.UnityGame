﻿using System.Collections;
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
        {Element,Tool,Weapon}

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
        public   Weaponhandler owner;
        
    
        [SerializeField]internal SphereCollider col;

        [SerializeField]internal  bool equipped;


    
        protected void init()
        {

            parentConstraint= GetComponent<ParentConstraint>();
            col = GetComponent<SphereCollider>();
        }






       public virtual void Equip()
       {
           if (!owner)
               return;
           else if (!owner.positionSettings.rightHand)
               return;
           //transform.SetParent(owner.positionSettings.rightHand);
           
           ConstraintSource cs = new ConstraintSource();
           cs.sourceTransform = owner.positionSettings.rightHand;
           cs.weight = 1;
           parentConstraint.AddSource(cs);
            Debug.Log(0000);
           parentConstraint.SetTranslationOffset(0,playerSettings.equipPosition);
           parentConstraint.SetRotationOffset(0,playerSettings.equipRotation);

           

           //transform.localPosition = playerSettings.equipPosition;
           //Quaternion equipRot = Quaternion.Euler(playerSettings.equipRotation);
           //transform.localRotation = equipRot;
       }








       //Unequips the weapon and places it to the desired location
       public virtual void  Unequip()
       {
           if (!owner)
               return;

        
           ConstraintSource cs = new ConstraintSource();
           cs.sourceTransform = owner.positionSettings.rightHand;
           cs.weight = 1;
        
           parentConstraint.AddSource(cs);

           parentConstraint.SetTranslationOffset(0,playerSettings.unequipPosition);
           parentConstraint.SetRotationOffset(0,playerSettings.unequipRotation);

       }





    }
}
