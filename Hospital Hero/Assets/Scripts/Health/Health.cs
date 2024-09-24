using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    private Animator animator;
    private bool isDead = false;
    private GameManager gM;
    private const string HealthKey = "PlayerHealth";
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.CompareTag("Player"))
        {
            if (PlayerPrefs.HasKey(HealthKey))
            {
                currentHealth = PlayerPrefs.GetInt(HealthKey);
            }
            else
            {
                currentHealth = maxHealth;
            }
        }
        else 
        {
            currentHealth = maxHealth;
        }
        
        gM = FindObjectOfType<GameManager>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void RestoreHealth(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        SavePlayerHealth();
    }
    void Die()
    {
        isDead = true;
        if (gameObject.CompareTag("Player"))
        {
            if (gM != null)
            {
                gM.TriggerPlayerDeath();
            }
        }
        else if (gameObject.CompareTag("Enemy"))
        {
             Destroy(gameObject);
        }
    }

    public void SavePlayerHealth()
    {
        if (gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetInt(HealthKey, currentHealth);
            PlayerPrefs.Save();
        }
    }

   
}
