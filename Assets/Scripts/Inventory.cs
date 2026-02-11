using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   // public TMPro.TextMeshPro diamondcounter;
    public Canvas KeyCollect;
    
    private int numberofdiamonds =0;
    private int costofdiamonds = 25;
     InputManager inputmanager;



    private void Awake()
    {
        inputmanager = FindObjectOfType<InputManager>();
  
        if(inputmanager == null )
        {
            return;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            KeyCollect.gameObject.SetActive(true);
            if (inputmanager.pickup_button)
            {
                numberofdiamonds = numberofdiamonds + costofdiamonds;
                Debug.Log(numberofdiamonds);
                Destroy(this.gameObject);
                inputmanager.pickup_button = false;
            }
        }
    }

   
}
