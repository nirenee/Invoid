using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Canvas ShopUI;
    // public GameObject Objecteyes;
    public ShopItemSO[] itemlist;
    public ShopTemplate[] shopCardslist;
    public TMP_Text ObjectTitle;
    public TMP_Text ObjectPrice;
    public TMP_Text ObjectDescription;
    private InputManager inputmanager;
    public Button Buy;
    public Inventory inventory;
    public float price;
    private void Awake()
    {
        inputmanager = FindObjectOfType<InputManager>();
        inventory = FindObjectOfType<Inventory>();
        if (inputmanager != null || inventory != null)
        {
            return;
        }
    }

    void Start()
    {
        ShopUI.gameObject.SetActive(false);
        LoadShopItems();
        ActiveItemstoPurchase();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        if (inputmanager.pickup_button){
            ShopUI.gameObject.SetActive(true);
            inputmanager.pickup_button = false;
        }
    }
    public void LoadShopItems()
    {
        for (int i = 0; i < itemlist.Length; i++)
        {
            shopCardslist[i].ItemTitle.text = itemlist[i].Title;
            shopCardslist[i].ItemDescription.text = itemlist[i].Description;
            shopCardslist[i].ItemPrice.text=  itemlist[i].Prize.ToString() + "gems";
            shopCardslist[i].ItemImg = itemlist[i].Image;
            int itemIndex = i;
            shopCardslist[i].Buy.onClick.AddListener(()=>BuyMode(itemIndex));
        }
    }
    public void ActiveItemstoPurchase()
    {
        for (int i = 0; i < itemlist.Length; i++)
        {
            if(inventory.totaldiamonds >= itemlist[i].Prize)
            {
                shopCardslist[i].Buy.interactable = true;
            }
            else
            {
                shopCardslist[i].Buy.interactable = false;
            }
        }
    }
    private void BuyMode(int index)
    {
        if(inventory.totaldiamonds >= itemlist[index].Prize)
        {
            inventory.totaldiamonds -= itemlist[index].Prize;
            Destroy(shopCardslist[index].gameObject);
            ActiveItemstoPurchase();
        }
       
    }
}
