using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponHandler : MonoBehaviour
{


#region Setting Classes
    [System.Serializable]
    public class PositionSettings
    {
        public Transform rightHand;
        public Transform LeftUnequipSpot;
        public Transform RightUnequipSpot;
    }
    

    [System.Serializable]
    public class AnimatorParameter
    {
        public string weaponTypeInt = "WeaponType";
        public string reloadingBool = "isReloading";
        public string aimingBool = "Aiming";
    }

#endregion Setting Classes

#region Variables
	

    [SerializeField]
	public PositionSettings positionSettings;

    [SerializeField]
    public AnimatorParameter animatorParameter;

	Animator animator;
	SoundController soundController;

    public Weapon currentWeapon;
    public List<Weapon> weaponsList = new List<Weapon>();
    public int maxWeapons = 2;
    public bool aim { get; protected set; }
    bool reload;
    int weaponType;
    bool settingWeapon;

#endregion Variables

    // Use this for initialization
    void OnEnable()
    {
		GameObject check = GameObject.FindGameObjectWithTag ("Sound Controller");
		if (check != null)
			soundController = check.GetComponent<SoundController> ();
        
		
		animator = GetComponent<Animator>();
		SetupWeapons ();
    }



    // Update is called once per frame
    void Update()
    {
        Animate();
    }
	

#region Methodes

	void SetupWeapons () {
		if (currentWeapon)
		{
			currentWeapon.SetEquipped(true);
			currentWeapon.SetOwner(this);
			AddWeaponToList(currentWeapon);

			if (currentWeapon.ammo.clipAmmo <= 0)
				Reload();

			if (reload)
			if (settingWeapon)
				reload = false;
		}

		if(weaponsList.Count > 0)
		{
			for(int i = 0; i < weaponsList.Count; i++)
			{
				if(weaponsList[i] != currentWeapon)
				{
					weaponsList[i].SetEquipped(false);
					weaponsList[i].SetOwner(this);
				}
			}
		}
	}


    //Animates the character
    void Animate()
    {
        if (!animator)
            return;

        animator.SetBool(animatorParameter.aimingBool, aim);
        animator.SetBool(animatorParameter.reloadingBool, reload);
        animator.SetInteger(animatorParameter.weaponTypeInt, weaponType);

        if (!currentWeapon)
        {
            weaponType = 0;
            return;
        }

		
		//**************************************************************
        switch (currentWeapon.weaponType)
        {
            case Weapon.WeaponType.Pistol:
                weaponType = 1;
                break;
            case Weapon.WeaponType.Rifle:
                weaponType = 2;
                break;
        }
    }


    //Adds a weapon to the weaponsList
    void AddWeaponToList(Weapon weapon)
    {
        if (weaponsList.Contains(weapon))
            return;

        weaponsList.Add(weapon);
    }




    //Puts the finger on the trigger and asks if we pulled
	public void FireCurrentWeapon (Ray aimRay)
    {
		if (currentWeapon.ammo.clipAmmo == 0) {
			Reload ();
			return;
		}

		currentWeapon.Fire (aimRay);
    }


    //Reloads the current weapon
    public void Reload()
    {
        if (reload || !currentWeapon)
            return;

        if (currentWeapon.ammo.carryingAmmo <= 0 || currentWeapon.ammo.clipAmmo == currentWeapon.ammo.maxClipAmmo)
            return;

		if (soundController != null) {
			if (currentWeapon.sounds.reloadSound != null) {
				if (currentWeapon.sounds.audioS != null) {
					soundController.PlaySound (currentWeapon.sounds.audioS, currentWeapon.sounds.reloadSound, true, currentWeapon.sounds.pitchMin, currentWeapon.sounds.pitchMax);
				}
			}
		}

        reload = true;
        StartCoroutine(StopReload());
    }


    //Stops the reloading of the weapon
    IEnumerator StopReload()
    {
        yield return new WaitForSeconds(currentWeapon.weaponSettings.reloadDuration);
        currentWeapon.LoadClip();
        reload = false;
    }

    //Sets out aim bool to be what we pass it
    public void Aim(bool aiming)
    {
        aim = aiming;
    }

    //Drops the current weapon
    public void DropCurWeapon()
    {
        if (!currentWeapon)
            return;

        currentWeapon.SetEquipped(false);
        currentWeapon.SetOwner(null);
        weaponsList.Remove(currentWeapon);
        currentWeapon = null;
    }

    //Switches to the next weapon
    public void SwitchWeapons()
	{
        if (settingWeapon || weaponsList.Count == 0)
            return;

        if (currentWeapon)
        {
            int currentWeaponIndex = weaponsList.IndexOf(currentWeapon);
            int nextWeaponIndex = (currentWeaponIndex + 1) % weaponsList.Count;

            currentWeapon = weaponsList[nextWeaponIndex];
        }
        else
        {
            currentWeapon = weaponsList[0];
        }
        settingWeapon = true;
        StartCoroutine(StopSettingWeapon());

		SetupWeapons ();
    }

    //Stops swapping weapons
    IEnumerator StopSettingWeapon()
    {
        yield return new WaitForSeconds(0.7f);
        settingWeapon = false;
    }

    void OnAnimatorIK()
    {
        if (!animator)
            return;

        if (currentWeapon && currentWeapon.userSettings.leftHandIKTarget && weaponType == 2 && !reload && !settingWeapon)
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
            Transform target = currentWeapon.userSettings.leftHandIKTarget;
            Vector3 targetPos = target.position;
            Quaternion targetRot = target.rotation;
            animator.SetIKPosition(AvatarIKGoal.LeftHand, targetPos);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, targetRot);
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
        }
    }

#endregion Methodes 
}
