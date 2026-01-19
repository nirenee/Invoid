using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{

    InputManager inputmanager;
    Playerlocomotion playerlocomotion;

    private void Awake()
    {
        inputmanager = GetComponent<InputManager>();
        playerlocomotion = GetComponent<Playerlocomotion>();
    }

    private void  Update()
    {
        inputmanager.HandleAllInputs();
    }


    private void FixedUpdate()
    {
        playerlocomotion.HandleAllMovement();
    }
}
