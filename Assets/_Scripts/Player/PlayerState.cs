using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThelastEngineering.Shared;


namespace ThelastEngineering.Player
{
public class PlayerState : StateClass {

#region Variables

   internal  bool jumping;
   internal bool wantToJump;
   internal  bool crouching;
   internal  bool running;

#endregion Variables


#region properties
    public bool Jumping
    {
        get
        {
            return jumping;
        }
        set
        {
            jumping = value;
        }
    }
	    public bool Crouching
    {
        get
        {
            return crouching;
        }
        set
        {
            crouching = value;
        }
    }

	    public bool Runnning
    {
        get
        {
            return running;
        }
        set
        {
            running = value;
        }
    }
	
#endregion properties



	// Awake is called when the script instance is being loaded.
	void Awake(){
		base.init();

	}
	
	// Update is called once per frame
	void Update () {
		base.update();
		
	}
}
}