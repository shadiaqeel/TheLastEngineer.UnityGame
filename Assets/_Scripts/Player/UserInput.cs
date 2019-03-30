using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using ThelastEngineering.Shared;
using ThelastEngineering.Inventory;


namespace ThelastEngineering.PlayerGroup
{
	

	public class UserInput : PlayerManager
	{


	#region Setting classes
	    [System.Serializable]
	    public class InputSettings
    {
        public string verticalAxis = "Vertical";
        public string horizontalAxis = "Horizontal";
        public string jumpButton = "Jump";
		public string crouchButton = "Crouch";
		public string runButton = "Run";

       // public string reloadButton = "Reload";
        public string aimButton = "Fire2";
        public string fireButton = "Fire1";
        //public string dropWeaponButton = "DropWeapon";
        public ButtonHandler switchWeaponBtn ;
		public ButtonHandler PickUpbtn ;
		public ButtonHandler firstWeaponbtn ;
		public ButtonHandler secondWeaponbtn ;
		public ButtonHandler releaseWeaponbtn;
		public ButtonHandler Attackbtn;
		
    }
	

	    [System.Serializable]
	    public class OtherSettings
    {
        public float lookSpeed = 5.0f;
        public float lookDistance = 30.0f;
        public bool requireInputForTurn = true;
        public LayerMask aimDetectionLayers;
    }
	#endregion Setting Classes







	#region Variables


		[SerializeField]
	    public InputSettings input;

	    [SerializeField]
	    public OtherSettings other;

	    public Camera TPSCamera;
	    public Transform spine;
	    bool aiming;





		public GameObject item;

		//Dictionary <Weapon, GameObject> crosshairPrefabMap = new Dictionary<Weapon, GameObject>();

	#endregion Variables









	#region initialization

	    // Use this for initialization
	    void Start()
	    {
			TPSCamera = Camera.main;
			

			//SetupCrosshairs ();
	    }

		// Update is called once per frame
	    void Update()
	    {
	        CharacterLogic();
	        CameraLookLogic();
	        WeaponLogic();
			ButtonsLogic();
			
	    }

	   /*  void LateUpdate()
	    {
	        if (weaponHandler)
	        {
	            if (weaponHandler.currentWeapon)
	            {
	                if (aiming)
	                   // PositionSpine();
	            }
	        }
	    }
	*/


	#endregion initialization








	#region Methodes

	/* 	void SetupCrosshairs () {
			if (weaponHandler.weaponsList.Count > 0) {
				foreach (Weapon wep in weaponHandler.weaponsList) {
					GameObject prefab = wep.weaponSettings.crosshairPrefab;
					if (prefab != null) {
						GameObject clone = (GameObject)Instantiate (prefab);
						crosshairPrefabMap.Add (wep, clone);
						ToggleCrosshair (false, wep);
					}
				}
			}
		}
	*/
	

	  #region  	Character
	    //Handles character logic
	    void CharacterLogic()
	    {
	        if (!player._movement)
	            return;

			Horizontal = CrossPlatformInputManager.GetAxis(input.horizontalAxis);
			Vertical = CrossPlatformInputManager.GetAxis(input.verticalAxis);

	


	        if (CrossPlatformInputManager.GetButtonDown(input.jumpButton))
	            wantToJump = true;
			if (CrossPlatformInputManager.GetButtonDown(input.crouchButton))
	            crouching=!crouching ;

			if (CrossPlatformInputManager.GetButtonDown(input.runButton))
	            running=true;
			if (CrossPlatformInputManager.GetButtonUp(input.runButton))
	            running=false;



	    }


	  #endregion Character







	  #region Camera
	    //Handles camera logic
	    void CameraLookLogic()
	    {
	        if (!TPSCamera)
	            return;

			other.requireInputForTurn = !aiming;

			if (other.requireInputForTurn) {
				if (CrossPlatformInputManager.GetAxis (input.horizontalAxis) != 0 || CrossPlatformInputManager.GetAxis (input.verticalAxis) != 0) {
					CharacterLook ();
				}
			}
			else {
				CharacterLook ();
			}
	    }


	

	    //Make the character look at a forward point from the camera
	    void CharacterLook()
	    {
	        Transform mainCamT = TPSCamera.transform;
	        Transform pivotT = mainCamT.parent;
	        Vector3 pivotPos = pivotT.position;
	        Vector3 lookTarget = pivotPos + (pivotT.forward * other.lookDistance);
	        Vector3 thisPos = transform.position;
	        Vector3 lookDir = lookTarget - thisPos;
	        Quaternion lookRot = Quaternion.LookRotation(lookDir);
	        lookRot.x = 0;
	        lookRot.z = 0;

	        Quaternion newRotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * other.lookSpeed);
	        transform.rotation = newRotation;
	    }


	  #endregion Camera

	/*	

		//Postions the spine when aiming

	void PositionSpine()
	    {
	        if (!spine || !weaponHandler.currentWeapon || !TPSCamera)
	            return;

	        Transform mainCamT = TPSCamera.transform;
	        Vector3 mainCamPos = mainCamT.position;
	        Vector3 dir = mainCamT.forward;
	        Ray ray = new Ray(mainCamPos, dir);

	        spine.LookAt(ray.GetPoint(50));

	        Vector3 eulerAngleOffset = weaponHandler.currentWeapon.userSettings.spineRotation;
	        spine.Rotate(eulerAngleOffset);
	    }
	*/
	    //Handles all weapon logic



	  #region Weapon 
	     void WeaponLogic()
	    {
			/* 
	        if (!weaponHandler)
	            return;

			aiming = CrossPlatformInputManager.GetButton (input.aimButton) ;
			//weaponHandler.Aim (aiming);

			if (CrossPlatformInputManager.GetButtonDown ("SwitchWeapon")) {
				weaponHandler.SwitchWeapons ();
				//UpdateCrosshairs ();
			}




			
			if (weaponHandler.currentWeapon) {

				Ray aimRay = new Ray (TPSCamera.transform.position, TPSCamera.transform.forward);

				//Debug.DrawRay (aimRay.origin, aimRay.direction);
				if (Input.GetButton (input.fireButton) && aiming)
					weaponHandler.FireCurrentWeapon (aimRay);
				if (Input.GetButtonDown (input.reloadButton))
					weaponHandler.Reload ();
				if (Input.GetButtonDown (input.dropWeaponButton)) {
					DeleteCrosshair (weaponHandler.currentWeapon);
					weaponHandler.DropCurWeapon ();
				}

				if (aiming) {
					ToggleCrosshair (true, weaponHandler.currentWeapon);
					PositionCrosshair (aimRay, weaponHandler.currentWeapon);
				}
				else
					ToggleCrosshair (false, weaponHandler.currentWeapon);
			} else
				TurnOffAllCrosshairs ();


				*/


	    }

		/* 

		void TurnOffAllCrosshairs () {
			foreach (Weapon wep in crosshairPrefabMap.Keys) {
				ToggleCrosshair (false, wep);
			}
		}

		void CreateCrosshair (Weapon wep) {
			GameObject prefab = wep.weaponSettings.crosshairPrefab;
			if (prefab != null) {
				prefab = Instantiate (prefab);
				ToggleCrosshair (false, wep);
			}
		}

		void DeleteCrosshair (Weapon wep) {
			if (!crosshairPrefabMap.ContainsKey (wep))
				return;

			Destroy (crosshairPrefabMap [wep]);
			crosshairPrefabMap.Remove (wep);
		}

		// Position the crosshair to the point that we are aiming
		void PositionCrosshair (Ray ray, Weapon wep)
		{
			Weapon curWeapon = weaponHandler.currentWeapon;
			if (curWeapon == null)
				return;
			if (!crosshairPrefabMap.ContainsKey (wep))
				return;

			GameObject crosshairPrefab = crosshairPrefabMap [wep];
			RaycastHit hit;
			Transform bSpawn = curWeapon.weaponSettings.bulletSpawn;
			Vector3 bSpawnPoint = bSpawn.position;
			Vector3 dir = ray.GetPoint(curWeapon.weaponSettings.range) - bSpawnPoint;

			if (Physics.Raycast (bSpawnPoint, dir, out hit, curWeapon.weaponSettings.range, 
				curWeapon.weaponSettings.bulletLayers)) {
				if (crosshairPrefab != null) {
					ToggleCrosshair (true, curWeapon);
					crosshairPrefab.transform.position = hit.point;
					crosshairPrefab.transform.LookAt (Camera.main.transform);
				}
			} else {
				ToggleCrosshair (false, curWeapon);
			}
		}

		// Toggle on and off the crosshair prefab
		void ToggleCrosshair(bool enabled, Weapon wep)
		{
			if (!crosshairPrefabMap.ContainsKey (wep))
				return;

			crosshairPrefabMap [wep].SetActive (enabled);
		}

		void UpdateCrosshairs () {
			if (weaponHandler.weaponsList.Count == 0)
				return;

			foreach (Weapon wep in weaponHandler.weaponsList) {
				if (wep != weaponHandler.currentWeapon) {
					ToggleCrosshair (false, wep);
				}
			}
		}

		*/




	  #endregion Weapon


	  #region Buttons
	  void ButtonsLogic()
	  {

		if (!player._handler)
	    return;

		  if(item != null  && item.GetComponent<Item>().owner==null)
		  {
			  input.PickUpbtn.gameObject.SetActive(true);
			  Text label =  input.PickUpbtn.GetComponentInChildren<Text>().GetComponent<Text>();
			  label.text = item.GetComponent<Item>().name;

			  if(CrossPlatformInputManager.GetButtonDown ("PickUp"))
			  	{	player._handler.PickUp(item.GetComponent<Item>());
				   	player._vision.RemoveItem(item.gameObject);
				 }
		  }else
		  {
 			input.PickUpbtn.gameObject.SetActive(false);
			 		 
		  }





			input.firstWeaponbtn.gameObject.SetActive(player._handler.firstWeapon != null);
			input.secondWeaponbtn.gameObject.SetActive(player._handler.secondWeapon != null);
			input.releaseWeaponbtn.gameObject.SetActive(player._handler.currentWeapon != null);


				if(CrossPlatformInputManager.GetButtonUp ("FirstWeapon"))
			  	{	player._handler.SwitchWeapons(1);
				 }
				
				if(CrossPlatformInputManager.GetButtonUp ("SecondWeapon"))
			  	{	player._handler.SwitchWeapons(2);
				 }

				if(CrossPlatformInputManager.GetButtonUp ("Release"))
			  	{	player._handler.Drop();
				 }

				if(CrossPlatformInputManager.GetButton("Fire"))
			  	{	player._handler.Fire(true);
				 }
				 else
				 {
					 player._handler.Fire(false);
				 }
				
		
		



	  }
	  #endregion Buttons


	 	
	#endregion Methodes




	}
}