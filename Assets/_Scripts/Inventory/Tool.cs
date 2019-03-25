using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ThelastEngineering.Inventory
{
    public class Tool : Item
    {

       // [System.Serializable]
        //public class Setting 
       // {
            

       // }



        /// Awake is called when the script instance is being loaded.
        void Awake()
        {
            base.init();
            base.Type =ItemType.Tool;
        }
    

    }
}
