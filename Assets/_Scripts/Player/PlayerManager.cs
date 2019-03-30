using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThelastEngineering.Shared;


namespace ThelastEngineering.PlayerGroup
{

[RequireComponent (typeof(Player))]
[RequireComponent(typeof(CharacterState))]
public class PlayerManager : MonoBehaviour {

    #region Variables

       protected static bool jumping;
       protected static bool wantToJump;
       protected static  bool crouching;
       protected static  bool running;

       protected  Player player;
       protected  CharacterState state;

    #endregion Variables


    #region properties

        protected  float Horizontal
        {
            get
            {
                return state.horizontal;
            }
            set
            {
                state.horizontal = value;
            }
        }
        protected float Vertical
        {
            get
            {
                return state.vertical;
            }
            set
            {
                state.vertical = value;
            }
        }
      



        public bool IsAlive
        {
            get
            {
                return state.isAlive;
            }
        }
    
    #endregion properties



    	// Awake is called when the script instance is being loaded.
    	void Awake(){
            player = GetComponent<Player>();
            state = GetComponent<CharacterState>();
            state.init();
            state.VisionState =true;
            

    	}
    
    	// Update is called once per frame
    	void Update () {
    
    	}
    }
}