using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CameraManager: MonoBehaviour
{
    InputManager inputManager;
    private Transform playerTransform;

    public Transform camerapivot;
    public Transform cameraAnchor;  
    public Transform cameratransform;

    public Slider camspeed;
    public float slidervalue;
    public float cameraOffsetX = 0.5f;
    public float speedCamera = 360f;
    public float minimumhigh = -35f;
    public float maximumhigh = 70f;
    public float cameraDistance = 4f;
    public float cameraCollisionRadius = 0.2f;
    public float CameraCollisionOffset = 0.2f;
    public float minCollisionOffset = 0.2f;
    public LayerMask collisionLayers;

    private float CameraUpDown;
    private float CameraLeftRight;
    private float defaultPosition;

    private void Awake()
    {
        playerTransform = FindObjectOfType<playerManager>().transform;
        inputManager = FindObjectOfType<InputManager>();
        defaultPosition = -cameraDistance;
    }

    public void InitCamera()
    {
        CameraUpDown = 0f;
        CameraLeftRight = 0f;
        cameratransform.position = cameraAnchor.position + cameraAnchor.rotation * new Vector3(0, 0, defaultPosition);
        cameratransform.rotation = cameraAnchor.rotation;
    }


    public void ChangeCameraSpeed(float value)
    {
        slidervalue = value;
        speedCamera = camspeed.value;
    }

    public void FollowPlayer()
    {
        
    }

    public void Rotate()
    {
        CameraUpDown += inputManager.cameraInput.x * speedCamera * Time.deltaTime;
        CameraLeftRight -= inputManager.cameraInput.y * speedCamera * Time.deltaTime;
        CameraLeftRight = Mathf.Clamp(CameraLeftRight, minimumhigh, maximumhigh);

        camerapivot.rotation = Quaternion.Euler(CameraLeftRight, CameraUpDown, 0f);

        cameratransform.position = cameraAnchor.position + cameraAnchor.rotation * new Vector3(0, 0, defaultPosition);
        cameratransform.rotation = cameraAnchor.rotation;
    }

    public void HandleCollisions()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = camerapivot.TransformDirection(Vector3.back);

        if (Physics.SphereCast(camerapivot.position, cameraCollisionRadius, direction,
                               out hit, Mathf.Abs(defaultPosition), collisionLayers))
        {
            float distance = Vector3.Distance(camerapivot.position, hit.point);
            targetPosition = -Mathf.Max(minCollisionOffset, distance - CameraCollisionOffset);
        }

        Vector3 finalPos = camerapivot.position + camerapivot.rotation * new Vector3(0, 0, targetPosition);
        cameratransform.position = Vector3.Lerp(cameratransform.position, finalPos, 0.2f);
        cameratransform.rotation = camerapivot.rotation;
    }

}
