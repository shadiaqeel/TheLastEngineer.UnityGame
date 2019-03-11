using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
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

    Animator animator;
    CharacterController characterController;

    [SerializeField]
    public AnimationSettings animations;

    [SerializeField]
    public PhysicsSettings physics;

   [SerializeField]
    public MovementSettings movement;
	Vector3 airControl;
	float vertical;
	float horizontal;
    bool jumping;
    bool crouching;
    bool running;

    bool resetGravity;
    float gravity;

#endregion Variables



    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.avatar = GetComponentsInChildren<Animator>()[1].avatar;     //Setup the animator with the child avatar

    }

    // Use this for initialization
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
		AirControl (vertical, horizontal);
        ApplyGravity();

        //isGrounded = characterController.isGrounded;
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
		if(Physics.SphereCast(start, radius, dir, out hit, characterController.height / 2, physics.groundLayers)) {
			return true;
		}

		return false;
	}

    //Animates the character and root motion handles the movement
    public void Animate(float vertical, float horizontal)
    {
		this.vertical = vertical;
		this.horizontal = horizontal;
        animator.SetFloat(animations.verticalVelocityFloat, vertical);
        animator.SetFloat(animations.horizontalVelocityFloat, horizontal);
		animator.SetBool(animations.groundedBool, isGrounded());
        animator.SetBool(animations.jumpBool, jumping);
        animator.SetBool(animations.crouchBool, crouching);
        animator.SetBool(animations.runBool, running);

    }

    public void Crouch()
    {
        crouching = !crouching;
    }

    public void Run(bool State)
    {
        running = State;
        
    }




    //Makes the character jump
    public void Jump()
    {
        if (jumping)
            return;

		if (isGrounded())
        {
            jumping = true;
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

    


#endregion Methodes
    
}
