using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Canvas ShopUI;
    public GameObject Objecteyes;
    private InputManager inputmanager;
    private void Awake()
    {
        inputmanager = FindObjectOfType<InputManager>();
        if (inputmanager != null)
        {
            return;
        }
    }

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }


    void Update()
    {
        
    }
}
