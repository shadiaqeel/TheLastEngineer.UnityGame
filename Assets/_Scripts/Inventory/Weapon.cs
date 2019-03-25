using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ThelastEngineering.Inventory
{
    public class Weapon : Item
    {


        void Awake()
        {
            base.init();
            base.Type =ItemType.Weapon;
        }
    
    
    }
}
