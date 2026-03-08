using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship_parts : MonoBehaviour
{
    public GameObject partofspaceship;
    public GameObject spaceshipopen;
    public GameObject spaceship;
    private InputManager inputManager;
    private Inventory inventory;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        inputManager = FindObjectOfType<InputManager>();
    }
    private void Update()
    {
        SpaceObjectActive();
    }
    public void SpaceObjectActive()
    {
        if (inventory.totaldiamonds >= 100)
        {
            partofspaceship.SetActive(true);
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (inputManager.pickup_button)
            {
                spaceship.SetActive(false);
                partofspaceship.SetActive(false);
                spaceshipopen.SetActive(true);
                inputManager.pickup_button = false;
                Destroy(this.gameObject);
            }

        }
    }
}
