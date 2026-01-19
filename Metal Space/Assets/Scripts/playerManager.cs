using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{

    InputManager inputmanager;
    Playerlocomotion playerlocomotion;
    CameraManager cameramanager;
    
    private void Awake()
    {
        inputmanager = GetComponent<InputManager>();
        playerlocomotion = GetComponent<Playerlocomotion>();
        cameramanager = FindObjectOfType<CameraManager>();
    
    }

    private void  Update()
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
    }
}
