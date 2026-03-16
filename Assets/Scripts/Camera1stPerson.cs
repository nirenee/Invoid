using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Camera1stPerson: MonoBehaviour
{
    InputManager inputManager;
    private Transform playertransform;
    public Transform camerapivot;
    public Transform cameratransform;
    public Slider camspeed;
    public float slidervalue;
    public float speedCamera= 360f;
    public float minimumhigh = -35;
    public float maximumhigh = 70;
    public float minangle = 45;
    public float maximumangle = 45;
    private float CameraUpDown;
    private float CameraLeftRight;
 
  

    private void Awake()
    {
        playertransform = FindObjectOfType<playerManager>().transform;
        inputManager = FindObjectOfType<InputManager>();
        cameratransform.position = camerapivot.position;
        cameratransform.rotation = camerapivot.rotation;
       
        
    }

    public void ChangeCameraSpeed(float value)
    {
        slidervalue = value;
        speedCamera = camspeed.value;
    }
  
   public void FollowPlayer()
    {
        cameratransform.position = camerapivot.position;
    }

    public void Rotate()
    {
        CameraUpDown = CameraUpDown + (inputManager.cameraInput.x * speedCamera * Time.deltaTime);
        CameraLeftRight = CameraLeftRight - ( inputManager.cameraInput.y * speedCamera * Time.deltaTime);
        CameraLeftRight = Mathf.Clamp(CameraLeftRight, minimumhigh, maximumhigh);
        
        Quaternion targetRotation = Quaternion.Euler( CameraLeftRight, CameraUpDown, 0f);
        camerapivot.rotation = targetRotation;

        cameratransform.position = camerapivot.position;
        cameratransform.rotation = camerapivot.rotation;
        

    }


}
