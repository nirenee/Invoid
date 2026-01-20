using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerController playerControls;
    Playerlocomotion playerlocomotion;
    AnimationManager animatormanager;
    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float verticalCamera;
    public float horizontalCamera;
    public float verticalInput;
    public float horizontalInput;
   
    private float amountmovement;

    public bool dash_button;
    public bool jumping_button;


    private void Awake()
    {
        animatormanager = GetComponent<AnimationManager>();
        playerlocomotion = GetComponent<Playerlocomotion>();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerController();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

            playerControls.PlayerActions.Jump.performed += i => jumping_button = true;

            playerControls.PlayerActions.Dash.performed += i => dash_button = true;
        }

        playerControls.Enable();
    }


    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleDashInput();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        verticalCamera = cameraInput.y;
        horizontalCamera = cameraInput.x;

        amountmovement = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatormanager.UpdateAnimator(horizontalInput, amountmovement);
    }
    private void HandleDashInput()
    {
        if (dash_button)
        {
            dash_button = false;
            playerlocomotion.HandleDash();
        }
    }

    private void HandleJumpInput()
    {
        if (jumping_button)
        {
            jumping_button = false;
            //Playerlocomotion.handlejump
        }
    }
}