using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ThelastEngineering.Shared
{


    public class StateClass : MonoBehaviour {


    #region Variables
    	protected Collider[] col;
        protected Rigidbody rb;

        protected float horizontal;
        protected float vertical;

        protected int health = 100;

        protected bool walking;
        protected bool idling;
        protected bool isAlive;

        public bool visionState;

    #endregion Variables

    #region properties
        public float Horizontal
        {
            get
            {
                return horizontal;
            }
            set
            {
                horizontal = value;
            }
        }

        public float Vertical
        {
            get
            {
                return vertical;
            }
            set
            {
                vertical = value;
            }
        }
        public bool Walking
        {
            get
            {
                return walking;
            }
            set
            {
                walking = value;
            }
        }

        public bool Idling
        {
            get
            {
                return idling;
            }
            set
            {
                idling = value;
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
        public void update () {
            if (Mathf.Abs(horizontal) > 0.0f || Mathf.Abs(vertical) > 0.0f)
                walking = true;
            else
                walking = false;
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
