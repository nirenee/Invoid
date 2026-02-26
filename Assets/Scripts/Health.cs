using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float currenthealth;
    public float MaxHealth;
    public float increaseHealth;
    public Slider healthslider;


    private void Start()
    {
      
        currenthealth = MaxHealth;
        SetSliderMax(MaxHealth);
        
    }
    public void SetSliderMax(float amount)
    {
        healthslider.maxValue = amount;
        SetSlider(amount);
    }

    public void SetSlider(float value)
    {
        healthslider.value = value;
    }
    public void ApplyDamage(float damage)
    {
        
        currenthealth = currenthealth - damage;
        SetSlider(currenthealth);

    }

    public void Heal(float totalheal)
    {
        currenthealth += totalheal;
        Debug.Log("heal");
    }
  
   /*private void OnCollisionEnter(Collision collision)
    {
      
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("collision");
            MaxHealth = MaxHealth - damage;
            SetSlider(MaxHealth);
        }
        if (MaxHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }*/
   
    


}
