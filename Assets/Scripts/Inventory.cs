using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Inventory : MonoBehaviour
{
   
    public TextMeshProUGUI counter;
    public float totaldiamonds;
  

    public void AddDiamonds(float amount)
    {
        if(counter == null)
        {
            return;
        }
        if(amount <= 0)
        {
            return;
        }
        totaldiamonds += amount;
        counter.text = totaldiamonds.ToString();
    }
    



}
