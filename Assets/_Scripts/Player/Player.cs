using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;
using ThelastEngineering.Shared;
using ThelastEngineering.PlayerGroup;


[RequireComponent(typeof(FootstepSounds))]
[RequireComponent(typeof(VisionSensor))]
[RequireComponent(typeof(Weaponhandler))]
[RequireComponent(typeof(CharacterMovement))]
[RequireComponent(typeof(UserInput))]
[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(GrounderFBBIK))]
[RequireComponent(typeof(FullBodyBipedIK))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{

    [HideInInspector] public Animator _anim;

    [HideInInspector] public UserInput _UInput;

    [HideInInspector] public CharacterMovement _movement;

    [HideInInspector] public VisionSensor _vision;

    [HideInInspector] public Weaponhandler _handler;

    public static Player instance;


    private void OnEnable()
    {
        this.hideFlags = HideFlags.HideInInspector;

    }

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _UInput = GetComponent<UserInput>();
        _movement = GetComponent<CharacterMovement>();
        _vision = GetComponent <VisionSensor>();
        _handler = GetComponent<Weaponhandler>();
        instance = this;

    }

    
}
