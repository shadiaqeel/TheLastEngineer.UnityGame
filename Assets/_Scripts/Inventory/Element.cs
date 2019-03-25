using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace ThelastEngineering.Inventory
{
    
    public class Element : Item
    {


        
         void Awake()
        {
            base.init();
            base.Type =ItemType.Element;
        }

        public override void Equip()
        {



        }

        public override void Unequip()
        {

            if (!owner)
            return;
            
            ConstraintSource cs = new ConstraintSource();
           cs.sourceTransform = owner.positionSettings.UnequipSpot;
           cs.weight = 1;
        
           parentConstraint.AddSource(cs);

           parentConstraint.SetTranslationOffset(0,playerSettings.unequipPosition);
           parentConstraint.SetRotationOffset(0,playerSettings.unequipRotation);

        }



    }

}
