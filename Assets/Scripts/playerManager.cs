using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    Animator animator;
    AnimationManager animationmanager;
    InputManager inputmanager;
    Playerlocomotion playerlocomotion;
    CameraManager camera3rdPerson;
    Camera1stPerson camera1stPerson;


    public bool isInteracting;
    public bool isJumping;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputmanager = GetComponent<InputManager>();
        playerlocomotion = GetComponent<Playerlocomotion>();
        camera3rdPerson = FindObjectOfType<CameraManager>();
        camera1stPerson = FindObjectOfType<Camera1stPerson>();
        animationmanager = GetComponent<AnimationManager>();
        SetThirdPersonCamera(true);

    }

    private void Update()
    {
        inputmanager.HandleAllInputs();
        if (inputmanager.cameraperspective)
        {
            inputmanager.cameraperspective = false;
            ToggleCameraMode();
        }
    }

    private void FixedUpdate()
    {
        playerlocomotion.HandleAllMovement();
    }

    private void ToggleCameraMode()
    {
        bool firstPersonOn = camera1stPerson != null && camera1stPerson.isActiveAndEnabled;
        SetFirstPersonCamera(!firstPersonOn);
    }

    public void SetFirstPersonCamera(bool enabled)
    {
        if (camera1stPerson != null)
        {
            camera1stPerson.enabled = enabled;
            camera1stPerson.gameObject.SetActive(enabled);
        }

        if (camera3rdPerson != null)
        {
            bool thirdEnabled = !enabled;
            camera3rdPerson.enabled = thirdEnabled;
            camera3rdPerson.gameObject.SetActive(thirdEnabled);
        }
    }

    public void SetThirdPersonCamera(bool enabled)
    {
        if (camera3rdPerson != null)
        {
            camera3rdPerson.enabled = enabled;
            camera3rdPerson.gameObject.SetActive(enabled);
        }

        if (camera1stPerson != null)
        {
            bool firstEnabled = !enabled;
            camera1stPerson.enabled = firstEnabled;
            camera1stPerson.gameObject.SetActive(firstEnabled);
        }
    }

    private void LateUpdate()
    {
        if (camera1stPerson != null && camera1stPerson.isActiveAndEnabled)
        {
            camera1stPerson.FollowPlayer();
            camera1stPerson.Rotate();
        }

        if (camera3rdPerson != null && camera3rdPerson.isActiveAndEnabled)
        {
            camera3rdPerson.FollowPlayer();
            camera3rdPerson.Rotate();
            camera3rdPerson.HandleCollisions();
        }

        isInteracting = animator.GetBool("isInteracting");
        playerlocomotion.isJumping = animator.GetBool("isJumping");
        animator.SetBool("isGrounded", playerlocomotion.isOnGround);
    }
}
