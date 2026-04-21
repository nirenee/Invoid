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
            Debug.Log(inventory.totaldiamonds);
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
            CanvasGroup cg = shopCardslist[index].GetComponent<CanvasGroup>();
            shopCardslist[index].Buy.gameObject.SetActive(false);
            if (cg == null) cg = shopCardslist[index].AddComponent<CanvasGroup>();
            cg.alpha = 0.5f;

            ActiveItemstoPurchase();
        }
       
    }
    private void UpgradeHabilities(int index)
    {
        if (bullet != null)
        {
            bullet.bulletdamage += itemlist[index].DamageBooster;
        }
        else
        {
            Debug.LogWarning("No se pudo mejorar el daño: No hay script Bullet en la escena.");
        }

        if (playerhealth != null) playerhealth.MaxHealth += itemlist[index].HealthBooster;
        if (playerspeed != null) playerspeed.moveSpeed += itemlist[index].speedBooster;
        if (bulletmanager != null) bulletmanager.bulletrange += itemlist[index].RangeBooster;
        if (attackspeed != null) attackspeed.cooldowntime += itemlist[index].AttackSpeedBooster;
    }
}
