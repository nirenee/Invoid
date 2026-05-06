using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Inventory : MonoBehaviour
{
   
    public TextMeshProUGUI counter;
    public GameManager gameManager;
    public AudioSource audiosource;
    public AudioClip gemcollect;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Start()
    {
        if (counter != null)
        {
            counter.text = GameManager.Instance.gemscount.ToString();
        }
    }
    public void UpdateCounter()
    {
        if (counter != null)
            counter.text = GameManager.Instance.gemscount.ToString();
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
        audiosource.PlayOneShot(gemcollect);
        GameManager.Instance.AddGems(amount);
     
        counter.text = GameManager.Instance.gemscount.ToString();
    }

}
