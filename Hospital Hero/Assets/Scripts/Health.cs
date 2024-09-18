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
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
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
    void Die()
    {
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

   
}
