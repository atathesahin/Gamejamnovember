using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Components")]
    public Animator enemyAnimator;
    public Transform player;
    private NavMeshAgent agent;
    private PlayerHealth playerHealth;
    [Header("Navmesh Settings")]
    public float detectionRadius = 15f;
    public float attackRadius = 2f;
    public float randomWalkRadius = 10f;
    public float randomWalkInterval = 5f;
    [Header("Attack Settings")]
    public int attackDamage = 10;
    public float attackInterval = 1.5f;
    [Header("Health Settings")]
    public int health = 100;
    
    private bool isAttacking = false;
    private float randomWalkTimer = 0f;
    private float attackTimer = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
       
            enabled = false;
            return;
        }

        if (enemyAnimator == null)
        {
            enemyAnimator = GetComponent<Animator>();
        }

        playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
        
        }
    }

    void Update()
    {
        if (!agent) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            // Player detected, move towards player
            agent.SetDestination(player.position);
            enemyAnimator.SetBool("isWalking", true);
            enemyAnimator.SetBool("isAttacking", false);
            isAttacking = false;

            if (distanceToPlayer <= attackRadius)
            {
                // Player within attack range, start attack
                agent.ResetPath();
                enemyAnimator.SetBool("isWalking", false);
                enemyAnimator.SetBool("isAttacking", true);
                isAttacking = true;

                // Handle attack
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackInterval)
                {
                    AttackPlayer();
                    attackTimer = 0f;
                }
            }
        }
        else
        {
            // Player not detected, random walk
            randomWalkTimer += Time.deltaTime;
            if (randomWalkTimer >= randomWalkInterval)
            {
                Vector3 randomDirection = Random.insideUnitSphere * randomWalkRadius;
                randomDirection += transform.position;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomDirection, out hit, randomWalkRadius, 1))
                {
                    agent.SetDestination(hit.position);
                    enemyAnimator.SetBool("isWalking", true);
                }
                randomWalkTimer = 0f;
            }
            enemyAnimator.SetBool("isAttacking", false);
            isAttacking = false;
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void AttackPlayer()
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    void OnDrawGizmosSelected()
    {
        // Draw detection radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // Draw attack radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}