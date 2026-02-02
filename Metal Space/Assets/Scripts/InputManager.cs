using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerController playerControls;
    Playerlocomotion playerlocomotion;
    AnimationManager animatormanager;
    BulletManager bulletmanager;
    public Vector2 movementInput;
    public Vector2 cameraInput;


    public float verticalCamera;
    public float horizontalCamera;
    public float verticalInput;
    public float horizontalInput;
   
    private float amountmovement;

    public bool dash_button;
    public bool jumping_button;
    public bool attack_button;

    private float time= 0.5f;

    private void Awake()
    {
        animatormanager = GetComponent<AnimationManager>();
        playerlocomotion = GetComponent<Playerlocomotion>();
        bulletmanager = FindObjectOfType<BulletManager>();
        
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

            playerControls.PlayerActions.Attack.performed += i => attack_button = true;
            
        }

        playerControls.Enable();
    }


    public void HandleAllInputs()
    {
        HandleJumpInput();

        HandleDashInput();
        HandleMovementInput();
        HandleAttack();

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
           
            playerlocomotion.HandleJumping();
        }
    }

    private void HandleAttack()
    {
        if (attack_button)
        {
           
            attack_button = false;
            
            bulletmanager.HandleBullet();

        }
       
    }

    private void StartVibration()
    {
        var gamepad = Gamepad.current;
        if (gamepad != null)
        {
            
            gamepad.SetMotorSpeeds(0.5f, 0.75f);
        }
    }

   
}