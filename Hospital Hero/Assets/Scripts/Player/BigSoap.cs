using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSoap : MonoBehaviour
{
    public int damage = 50;
    public float maxDistance = 15.0f;
    public int maxHits = 2;
    private Vector2 startPos;
    private int hitCount = 0;

    public GameObject bubblePrefab;
    public float bubbleSpawnInterval = .2f;
    public float bubbleSpawnRadius = 0.5f;
    public int minBubbles = 5;
    public int maxBubbles = 15;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceTraveled = Vector2.Distance(startPos, transform.position);   

        if (distanceTraveled > maxDistance)
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
            hitCount++;
            if (hitCount >= maxHits)
            {
                Destroy(gameObject);
            }
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
