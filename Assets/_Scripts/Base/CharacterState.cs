using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ThelastEngineering.Shared
{


    public  class CharacterState : MonoBehaviour {


    #region Variables
    	[HideInInspector]public  Collider[] col;
        [HideInInspector]public  Rigidbody rb;

        [HideInInspector]public  float horizontal;
        [HideInInspector]public  float vertical;

        [HideInInspector]public  int health = 100;

        [HideInInspector]public  bool walking;
        [HideInInspector]public  bool idling;
       [HideInInspector] public  bool isAlive;

        private  bool visionState;

    #endregion Variables

    #region properties

        public  bool VisionState
        {
            get
            {
                return visionState;
            }
            set
            {
                visionState = value;
            }
        }
        public bool IsAlive
        {
            get
            {
                return isAlive;
            }
        }
        


    #endregion properties

        // Use this for initialization
        public void init () {
            col = GetComponents<Collider>();
            rb = GetComponent<Rigidbody>();
            isAlive = true;
           
    	}

        // Update is called once per frame
        public void update() {
        }

        public void takeDamage(int dmg, GameObject test)
        {
            health -= dmg;
            if (health <= 0)
            {
                health = 0;
                isAlive = false;
                die();
            }
        }

        public void die()
        {
            for (int i = 0; i < col.Length; ++i)
                col[i].enabled = false;
            rb.isKinematic = true;
        }
    }


}
