using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


namespace ThelastEngineering.Inventory
{

    public class Weapon : Item
    {

        public int orderOfWeapon;

        void Awake()
        {
            base.init();
            base.Type =ItemType.Weapon;
        }

        public override void Equip()
        {



        }
        public override void Unequip()
        {
            if (!owner)
            return;


            ConstraintSource cs = new ConstraintSource();



            if(orderOfWeapon==1)
            {

            cs.sourceTransform = owner.positionSettings.FirstUnequipWeaponSpot;
            cs.weight = 1;
            parentConstraint.AddSource(cs);


            }
            else
            {
            cs.sourceTransform = owner.positionSettings.SecondUnequipWeaponSpot;
            cs.weight = 1;
            parentConstraint.AddSource(cs);  
            }

            parentConstraint.SetTranslationOffset(0,playerSettings.unequipPosition);
            parentConstraint.SetRotationOffset(0,playerSettings.unequipRotation);

        }
    
    
    }
}
