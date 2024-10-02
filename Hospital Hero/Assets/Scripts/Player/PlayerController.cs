using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private float horizontalInput;
    public float speed = 10;
    public float jumpForce = 5;
    private bool facingRight = true;

    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundlayer;
    private bool isGrounded;

    //mellee properties
    public int meleeDamage = 20;
    public float attackRange = 0.5f;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    //ranged properties
    public GameObject shurikenPrefab;
    public Transform shurikenSpawnPoint;
    public float shurikenSpeed = 10f;
    public int maxSoap = 5;
    private int availableSoap;
    public float soapReloadTime;
    private float[] soapCooldownTimers;

    public GameObject[] soapUI;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        availableSoap = maxSoap;
        soapCooldownTimers = new float[maxSoap];
        UpdateSoapUI();
       
    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundlayer);
        animator.SetBool("isGrounded", isGrounded);

        for (int i = 0; i < maxSoap; i++)
        {
            if (soapCooldownTimers[i] > 0)
            {
                soapCooldownTimers[i] -= Time.deltaTime;
                if (soapCooldownTimers[i] <= 0)
                {
                    availableSoap++;
                    UpdateSoapUI();
                }
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            

        }

        HandleJump();

        horizontalInput = Input.GetAxis("Horizontal");

        if (isGrounded)
        {

            animator.SetBool("isRunning", horizontalInput != 0);
        } 
            if (horizontalInput != 0)
            {

                if (horizontalInput > 0 && !facingRight)
                {
                    Flip();
                }
                else if (horizontalInput < 0 && facingRight)
                {
                    Flip();
                }
            }
        
            transform.Translate(Vector2.right * horizontalInput * speed * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.E))
            {
                //MeleeAttack();
                animator.SetTrigger("melee");
            }

            if (Input.GetKeyDown(KeyCode.F) && availableSoap > 0)
            {
                ThrowSoap();
                

            }
        
    }

    private void HandleJump()
    {
        float verticalVelocity = playerRb.velocity.y;

        if (verticalVelocity > 0.1 && !isGrounded)
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isFalling", false);
        }
        else if (verticalVelocity < -0.1 && !isGrounded)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        }
        else if (isGrounded)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
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

    void ThrowSoap()
    {
            if (availableSoap > 0)
        {
            availableSoap--;

            for(int i = 0; i < maxSoap; i++)
            {
                if (soapCooldownTimers[i] <= 0)
                {
                    soapCooldownTimers[i] = soapReloadTime;
                    break;
                }
            }
        }
        
            GameObject shuriken = Instantiate(shurikenPrefab, shurikenSpawnPoint.position, shurikenSpawnPoint.rotation);
            Rigidbody2D rb = shuriken.GetComponent<Rigidbody2D>();

            Vector2 shurikenDirecton = facingRight ? Vector2.right : Vector2.left;
            rb.velocity = shurikenDirecton * shurikenSpeed;

            float rotationDirection = transform.localScale.x > 0 ? -300f : 300f;
            rb.angularVelocity = rotationDirection;

        UpdateSoapUI();
        
    }

    

    void UpdateSoapUI()
    {
        for (int i = 0; i < maxSoap; i++)
        {
            if (i < availableSoap)
            {
                soapUI[i].SetActive(true);
            }
            else
            {
                soapUI[i].SetActive(false);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Vector2 attackDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        Gizmos.DrawLine(attackPoint.position, attackPoint.position + (Vector3)attackDirection * attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
