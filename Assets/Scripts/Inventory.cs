using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Inventory : MonoBehaviour
{
   
    public TextMeshProUGUI counter;
    public float totaldiamonds;

    public void Awake()
    {
          totaldiamonds = GameManager.Instance.gemscount;
    }
    public void Start()
    {
        if (counter != null)
        {
            counter.text = totaldiamonds.ToString();
        }
    }
    public void AddDiamonds(int amount)
    {
        if(counter == null)
        {
            return;
        }
        if(amount <= 0)
        {
            return;
        }
         GameManager.Instance.AddGems(amount);
        totaldiamonds = GameManager.Instance.gemscount;
        counter.text = totaldiamonds.ToString();
    }

}
