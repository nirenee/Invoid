using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShopObject", menuName = "Shop/Item")]
public class ShopItemSO : ScriptableObject
{
    public string Title;
    public Sprite Image;
    public int Prize;
    public string Description;
 
    [Header("Stats")]
    public float speedBooster;
    public float DamageBooster;
    public float RangeBooster;
    public float AttackSpeedBooster;
    public float HealthBooster;



}
