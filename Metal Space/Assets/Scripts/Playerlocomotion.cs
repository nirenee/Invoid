using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerlocomotion : MonoBehaviour
{
    InputManager inputmanager;
    Vector3 directionmove;
    Transform camera;
    Rigidbody PlayerRB;

    public TrailRenderer tr;
    public float moveSpeed = 7;
    public float rotationSpeed;
    public bool isDashing ;
    public float dashvelocity = 30f;
    public float dashtime = 0.3f;
    public float dashcooldown = 1f;
    private float lastDashTime = -Mathf.Infinity;


    public void HandleAllMovement()
    {
       
        HandleMovement();
        HandleRotation();
        
    }


    private void Awake()
    {
        tr.emitting = false;
        inputmanager = GetComponent<InputManager>();
        PlayerRB = GetComponent<Rigidbody>();
        camera = Camera.main.transform;
    }


      public void HandleDash()
    {
        if (Time.time >= lastDashTime + dashcooldown && !isDashing)
        {
            StartCoroutine(Dash());
        }
       
    }

    private void HandleMovement()
    {
       
         if (isDashing){
          return;   
         }
        directionmove = camera.forward * inputmanager.verticalInput;
        directionmove = directionmove + camera.right * inputmanager.horizontalInput;
        directionmove.Normalize();
        directionmove.y = 0;
        directionmove = directionmove * moveSpeed;

        Vector3 movementVelocity = directionmove;
        PlayerRB.velocity =  movementVelocity;
      
    }

  

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection= camera.forward * inputmanager.verticalInput;
        targetDirection = targetDirection + camera.right * inputmanager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;
       
        if(targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation,targetRotation, rotationSpeed * Time.deltaTime);
   
        transform.rotation = playerRotation;
    
    }
    private IEnumerator Dash()
    {
        isDashing = true;
        lastDashTime = Time.time;
        tr.emitting = true;
      
        Vector3 dashDirection = transform.forward;

        float startTime = Time.time;

        while (Time.time < startTime + dashtime)
        {
            PlayerRB.velocity = dashDirection * dashvelocity;
            yield return null;
        }
        tr.emitting = false;
        isDashing = false;
       
    }
    public void HandleJumping()
    {

    }



}
