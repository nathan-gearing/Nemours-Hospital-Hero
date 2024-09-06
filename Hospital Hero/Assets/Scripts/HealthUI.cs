using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Health playerHealth;
    public Slider healthSlider;
    public float smoothSpeed = 20f;
    

   
    void Update()
    {
        healthSlider.value = Mathf.Lerp(healthSlider.value, playerHealth.currentHealth, smoothSpeed * Time.deltaTime);
       
    }
}
