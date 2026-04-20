using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Canvas ShopUI;
    public ShopItemSO[] itemlist;
    public ShopTemplate[] shopCardslist;
    private InputManager inputmanager;
    public Button Buy;

    public Inventory inventory;
    private Health playerhealth;
    private Bullet bullet;
    private BulletManager bulletmanager;
    private Playerlocomotion playerspeed;
    private InputManager attackspeed;
    public float price;
    private void Awake()
    {
        inputmanager = FindObjectOfType<InputManager>();
        inventory = FindObjectOfType<Inventory>();
        playerhealth = FindObjectOfType<Health>();
        bullet = FindObjectOfType<Bullet>();
        bulletmanager = FindObjectOfType<BulletManager>();
        playerspeed = FindObjectOfType<Playerlocomotion>();
        attackspeed = FindObjectOfType<InputManager>();
        if (inputmanager == null || inventory == null || playerhealth == null )
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
            UpgradeHabilities(index);
            Destroy(shopCardslist[index].gameObject);
            ActiveItemstoPurchase();
        }
       
    }
    private void UpgradeHabilities(int index)
    {
        bullet.bulletdamage += itemlist[index].DamageBooster;
        playerhealth.MaxHealth += itemlist[index].HealthBooster;
        playerspeed.moveSpeed += itemlist[index].speedBooster;
        bulletmanager.bulletrange += itemlist[index].RangeBooster;
        attackspeed.cooldowntime += itemlist[index].AttackSpeedBooster;

}
}
