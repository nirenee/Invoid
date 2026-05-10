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
    public GameManager gameManager;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        inputManager = FindObjectOfType<InputManager>();
        gameManager = FindObjectOfType<GameManager>();
        GameObject spaceshipParent = GameObject.FindWithTag("SpaceshipOpenParent");
    }
    private void Start()
    {
        GameObject spaceshipParent = GameObject.FindWithTag("SpaceshipOpenParent");
        spaceship = GameObject.FindWithTag("Spaceship");
        
    }

    private void Update()
    {
        if (spaceship == null)
            spaceship = GameObject.FindWithTag("Spaceship");

        if (spaceshipopen == null)
            spaceshipopen = GameObject.FindWithTag("SpaceshipOpen");
        SpaceObjectActive();

    }
    public void SpaceObjectActive()
    {
        if (GameManager.Instance.gemscount >= GameManager.Instance.gemsRequired)
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
