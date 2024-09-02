using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //mellee properties
    public int meleeDamage = 20;
    public float attackRange = 0.5f;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    //ranged properties
    public GameObject shurikenPrefab;
    public Transform shurikenSpawnPoint;
    public float shurikenSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            MeleeAttack();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            ThrowShuriken();
        }
    }

    void MeleeAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Health>().TakeDamage(meleeDamage);
        }
    }

    void ThrowShuriken()
    {
        GameObject shuriken = Instantiate(shurikenPrefab, shurikenSpawnPoint.position, shurikenSpawnPoint.rotation );
        Rigidbody2D rb = shuriken.GetComponent<Rigidbody2D>();
        rb.velocity = transform.localScale.x * shurikenSpawnPoint.right * shurikenSpeed * Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
