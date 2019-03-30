using UnityEngine;
using System.Collections;
using System;
using ThelastEngineering.Shared;



namespace ThelastEngineering.PlayerGroup
{
    
    public class CharacterMovement : PlayerManager
    {




    #region Setting Classes


        [System.Serializable]
        public class AnimationSettings
        {
            public string verticalVelocityFloat = "Vertical";
            public string horizontalVelocityFloat = "Horizontal";
            public string groundedBool = "IsGrounded";
            public string jumpBool = "IsJumping";
            public string crouchBool = "IsCrouched";
            public string runBool = "IsRunning";

            [Header("LookAt")]
            [Range(0,1f)] public float lookIkweight;
            [Range(0,1f)] public float bodyIkweight;
            [Range(0,1f)] public float headweight;
            [Range(0,1f)] public float eyeweight;
            [Range(0,1f)] public float clampweight;

            public Transform lookatTarget;



        }



        [System.Serializable]
        public class PhysicsSettings
        {
            public float gravityModifier = 9.81f;
            public float baseGravity = 50.0f;
            public float resetGravityValue = 1.2f;
    		public LayerMask groundLayers;
    		public float airSpeed = 2.5f;
        }


        [System.Serializable]
        public class MovementSettings
        {
            public float jumpSpeed = 6;
            public float jumpTime = 0.25f;
        }
    


    #endregion Setting Classes 

    #region Variables

        CharacterController characterController;

        [SerializeField]
        public AnimationSettings animations;

        [SerializeField]
        public PhysicsSettings physics;

       [SerializeField]
        public MovementSettings movement;

    	Vector3 airControl;
    	//float vertical;
    	//float horizontal;


        bool resetGravity;
        float gravity;

    #endregion Variables






        // Use this for initialization
        void Start()
        {   
            //Setup the animator with the child avatar
            player._anim.avatar = GetComponentsInChildren<Animator>()[1].avatar;    
            characterController = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            if(IsAlive)
            {
                Animate();
                AirControl (Vertical, Horizontal);
                ApplyGravity();


                if(wantToJump)
                {
                    Jump();
                    wantToJump=false;
                }


            }
        }








    #region Methodes


        void AirControl(float vertical, float horizontal) {
    		if (!isGrounded()) {
    			airControl.x = horizontal;
    			airControl.z = vertical;
    			airControl = transform.TransformDirection (airControl);
    			airControl *= physics.airSpeed;

    			characterController.Move (airControl * Time.deltaTime);
    		}
    	}





        bool isGrounded () {
    		RaycastHit hit;
    		Vector3 start = transform.position + transform.up;
    		Vector3 dir = Vector3.down;
    		float radius = characterController.radius;
    		if(Physics.SphereCast(start, radius, dir, out hit, 2f, physics.groundLayers)) {
    			return true;
    		}

    		return false;
    	}



        //Animates the character and root motion handles the movement
        public void Animate()
        {

            player._anim.SetFloat(animations.verticalVelocityFloat, Vertical );
            player._anim.SetFloat(animations.horizontalVelocityFloat, Horizontal);
    		player._anim.SetBool(animations.groundedBool, isGrounded());
            player._anim.SetBool(animations.jumpBool, jumping);
            player._anim.SetBool(animations.crouchBool, crouching);
            player._anim.SetBool(animations.runBool, running);

        }















    #region  Jumping

        //Makes the character jump
        public void Jump()
        {
            if (jumping)
                return;

    		if (isGrounded())
            {
                jumping = true;
                physics.airSpeed = Vector3.Magnitude( new Vector3(player._anim.velocity.x,0,player._anim.velocity.z))*1.5f;
                StartCoroutine(StopJump());
            }
        }

        //Stops us from jumping
        IEnumerator StopJump()
        {
            yield return new WaitForSeconds(movement.jumpTime);
            jumping = false;
        }

        //Apply downward force to the character when we aren't jumping
        void ApplyGravity()
        {
    		if (!isGrounded())
            {
                if (!resetGravity)
                {
                    gravity = physics.resetGravityValue;
                    resetGravity = true;
                }
                gravity += Time.deltaTime * physics.gravityModifier;
            }
            else
            {
                gravity = physics.baseGravity;
                resetGravity = false;
            }

            Vector3 gravityVector = new Vector3();

            if (!jumping)
            {
                gravityVector.y -= gravity;
            }
            else
            {
                gravityVector.y = movement.jumpSpeed;
            }

            characterController.Move(gravityVector * Time.deltaTime);
        }


    #endregion Jumping


    #endregion Methodes

     void OnAnimatorIK()
    {
        player._anim.SetLookAtWeight(animations.lookIkweight,animations.bodyIkweight,animations.headweight,animations.eyeweight,animations.clampweight);
        player._anim.SetLookAtPosition(animations.lookatTarget.position);  


  

    }



    }

}