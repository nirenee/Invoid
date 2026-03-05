using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spaceship_scenechange : MonoBehaviour
{
    private InputManager inputManager;
    public string sceneName;

    private void Awake()
    {
        inputManager = FindObjectOfType < InputManager>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (inputManager.pickup_button)
            {
                SceneManager.LoadScene(sceneName);
               
            }
           
        }
    }    

}
