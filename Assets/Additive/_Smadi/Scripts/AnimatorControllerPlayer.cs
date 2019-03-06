using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControllerPlayer : MonoBehaviour {

    public FloatingJoystick leftJoystick;
    public FloatingJoystick rightJoystick;

    Animator animator;
    // Use this for initialization
    void Start () {
        animator = gameObject.gameObject.GetComponent(typeof(Animator)) as Animator;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
