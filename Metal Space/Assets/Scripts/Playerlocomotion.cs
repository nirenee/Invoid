using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerlocomotion : MonoBehaviour
{
    InputManager inputmanager;
    Vector3 directionmove;
    Transform camera;
    Rigidbody PlayerRB;
    

    public float moveSpeed = 7;
    public float rotationSpeed;

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }


    private void Awake()
    {
        inputmanager = GetComponent<InputManager>();
        PlayerRB = GetComponent<Rigidbody>();
        camera = Camera.main.transform;
    }

    private void HandleMovement()
    {
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



}
