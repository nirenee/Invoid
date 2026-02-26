using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class pickupdiamonds: MonoBehaviour
{
    public Canvas KeyCollect;
    
    public int costofdiamonds = 25;
    InputManager inputmanager;
    Inventory inventory;

    private void Awake()
    {
        inputmanager = FindObjectOfType<InputManager>();
        inventory = FindObjectOfType<Inventory>();
        if(inputmanager == null || inventory == null )
        {
            return;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            KeyCollect.gameObject.SetActive(true);
      
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
          if (inputmanager.pickup_button)
            {
                inventory.AddDiamonds(costofdiamonds);
                inputmanager.pickup_button = false;
                Destroy(this.gameObject);
            }

        }
     }

}
