using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Health playerHealth;
    public Slider healthSlider;
    void Update()
    {
        healthSlider.value = playerHealth.currentHealth;
    }
}
