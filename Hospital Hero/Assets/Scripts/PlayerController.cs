using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private SpriteRenderer spriteRenderer;
    private float horizontalInput;
    public float speed = 10;
    public float jumpForce = 5;
    private bool facingRight = true;

    //mellee properties
    public int meleeDamage = 20;
    public float attackRange = 0.5f;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    //ranged properties
    public GameObject shurikenPrefab;
    public Transform shurikenSpawnPoint;
    public float shurikenSpeed = 10f;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
       
    }
    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space)) {
            
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        }
        horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            if (horizontalInput > 0 && !facingRight)
            {
                Flip();
            } else if (horizontalInput < 0 && facingRight)
            {
                Flip();
            }
        }
       transform.Translate(Vector2.right * horizontalInput * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.E))
        {
            MeleeAttack();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            ThrowShuriken();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
    void MeleeAttack()
    {
        Vector2 attackDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        
        RaycastHit2D hit = Physics2D.Raycast(attackPoint.position, attackDirection, attackRange, enemyLayers);

        if (hit.collider != null)
        {
            Health enemyHealth = hit.collider.GetComponent<Health>();
            Debug.Log("Hit: " + hit.collider.name);
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(meleeDamage);
            }
        }
        else
        {
            Debug.Log("not in range");
        }

    }

    void ThrowShuriken()
    {
        GameObject shuriken = Instantiate(shurikenPrefab, shurikenSpawnPoint.position, shurikenSpawnPoint.rotation);
        Rigidbody2D rb = shuriken.GetComponent<Rigidbody2D>();

        if (transform.localScale.x > 0)
        {
            rb.velocity = shurikenSpawnPoint.right * shurikenSpeed;
        } else
        {
            rb.velocity = shurikenSpawnPoint.right * -shurikenSpeed;
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Vector2 attackDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Gizmos.DrawLine(attackPoint.position, attackPoint.position + (Vector3)attackDirection * attackRange);
    }
}
