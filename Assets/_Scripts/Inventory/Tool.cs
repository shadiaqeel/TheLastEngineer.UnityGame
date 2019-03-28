using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


namespace ThelastEngineering.Inventory
{
    public class Tool : Item
    {




        /// Awake is called when the script instance is being loaded.
        void Awake()
        {
            base.init();
            Type =ItemType.Tool;
        }
    

        public override void Unequip()
        {
            if (!owner)
            return;
            
            ConstraintSource cs = new ConstraintSource();
           cs.sourceTransform = owner.positionSettings.ToolUnequipWeaponSpot;
           cs.weight = 1;
        
           parentConstraint.AddSource(cs);

           parentConstraint.SetTranslationOffset(0,playerSettings.unequipPosition);
           parentConstraint.SetRotationOffset(0,playerSettings.unequipRotation);
            
        }


    }
}
