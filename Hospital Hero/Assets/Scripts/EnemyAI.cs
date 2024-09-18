using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    public float speed = 3.0f;
    public float detectionRange = 10.0f;
    public float attackRange = 1.5f;
    public float stopRange = 2.0f;
    public float attackCoolDown = 2.0f;
    public float nextAttackTime = 0f;
    public int damage = 10;
    private Health playerHealth;
    private Animator enemyAnimator;
    private Animator playerAnimator;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        playerHealth = player.GetComponent<Health>();
        enemyAnimator = GetComponent<Animator>();
        playerAnimator = player.GetComponent <Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float verticalDifference = Mathf.Abs(transform.position.y - player.position.y);

        if (distanceToPlayer < detectionRange)
        {
            if (distanceToPlayer > stopRange)
            {
                MoveTowardsPlayer();
            }

            if (distanceToPlayer <= attackRange && verticalDifference <= 1.0f && Time.time >= nextAttackTime)
                        {
                            AttackPlayer();
                            nextAttackTime = Time.time + attackCoolDown;
                        }



            

        }



    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    private void AttackPlayer()
    {
        int randomAttack = Random.Range(0, 2);
        switch (randomAttack)
        {
            case 0:
                enemyAnimator.SetTrigger("attack1");
                break;
            case 1:
                enemyAnimator.SetTrigger("attack2");
                break;
            case 2:
                enemyAnimator.SetTrigger("attack3");
                break;

        }
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            playerAnimator.SetTrigger("hit");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stopRange);
    }
}
