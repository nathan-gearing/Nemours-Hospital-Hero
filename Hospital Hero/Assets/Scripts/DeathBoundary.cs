using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBoundary : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Debug.Log("PLayer fell out of bounds");
            gameManager.TriggerPlayerDeath();
        }
    }
}
