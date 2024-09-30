using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Shuriken : MonoBehaviour
{
    public int damage = 10;
    public float maxDistance = 10.0f;
    private Vector2 startPosition;

    public GameObject bubblePrefab;
    public float bubbleSpawnInterval = .2f;
    public float bubbleSpawnRadius = 0.5f;
    public int minBubbles = 5;
    public int maxBubbles = 15;

    private void Start()
    {
        startPosition = transform.position;
        StartCoroutine(SpawnBubbles());
    }
    private void Update()
    {
        float distanceTraveled = Vector2.Distance(startPosition, transform.position);

        if (distanceTraveled >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("hit enemy");
            Health enemyHealth = collision.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    IEnumerator SpawnBubbles()
    {
        while (true)
        {
            int randomBubbleCount = Random.Range(minBubbles, maxBubbles);

            for (int i = 0; i < randomBubbleCount; i++)
            {
                Vector3 spawnPos = transform.position + (Vector3)Random.insideUnitCircle * bubbleSpawnRadius;
                Instantiate(bubblePrefab, spawnPos, Quaternion.identity);
            }

            yield return new WaitForSeconds(bubbleSpawnInterval);
        }
    }
   
}
