using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    Animator animator;
    AnimationManager animationmanager;
    InputManager inputmanager;
    Playerlocomotion playerlocomotion;
    CameraManager cameramanager;

    public bool isInteracting;
    public bool isJumping;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputmanager = GetComponent<InputManager>();
        playerlocomotion = GetComponent<Playerlocomotion>();
        cameramanager = FindObjectOfType<CameraManager>();
        animationmanager = GetComponent<AnimationManager>();
    }

    private void Update()
    {
        inputmanager.HandleAllInputs();
    }


    private void FixedUpdate()
    {
        playerlocomotion.HandleAllMovement();

    }

    private void LateUpdate()
    {
        cameramanager.FollowPlayer();
        cameramanager.Rotate();
        isInteracting = animator.GetBool("isInteracting");
        playerlocomotion.isJumping = animator.GetBool("isJumping");
        animator.SetBool("isGrounded", playerlocomotion.isOnGround);
    }
}
