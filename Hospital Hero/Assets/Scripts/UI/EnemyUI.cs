using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    public Health enemyHealth;
    public Slider enemySlider;
    public float smoothSpeed = 5f;
    // Update is called once per frame
    void Update()
    {
        enemySlider.value = Mathf.Lerp(enemySlider.value, enemyHealth.currentHealth, smoothSpeed * Time.deltaTime);
    }
}
