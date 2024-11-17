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

    [Header("Speed Settings")]
    public float baseSpeed = 2.5f; 
    public float speedIncreaseAmount = 2f;

    private bool isAggressive = false; 
    private bool hasIncreasedSpeed = false; 
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

        // Başlangıç hızını ayarla
        agent.speed = baseSpeed;

        if (enemyAnimator == null)
        {
            enemyAnimator = GetComponent<Animator>();
        }

        playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth == null)
        {
            Debug.LogWarning("PlayerHealth script'i atanmadı!");
        }
    }

    void Update()
    {
        if (!agent) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (isAggressive || distanceToPlayer <= detectionRadius)
        {
            agent.SetDestination(player.position);
            enemyAnimator.SetBool("isWalking", true);
            enemyAnimator.SetBool("isAttacking", false);
            isAttacking = false;

            if (distanceToPlayer <= attackRadius)
            {
                agent.ResetPath();
                enemyAnimator.SetBool("isWalking", false);
                enemyAnimator.SetBool("isAttacking", true);
                isAttacking = true;

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

    public void TakeDamage(int damageAmount)
    {
        Debug.Log("Damage alındı: " + damageAmount);
        health -= damageAmount;

        if (health > 0)
        {
            BecomeAggressive(); 
            IncreaseSpeed(); 
        }
        else
        {
            Die();
        }
    }

    private void IncreaseSpeed()
    {
        if (!hasIncreasedSpeed)
        {
            agent.speed += speedIncreaseAmount; // Hızı artır
            hasIncreasedSpeed = true; // Hız artırıldığı işaretlenir
            Debug.Log($"Düşmanın hızı artırıldı: {agent.speed}");
        }
    }

    private void BecomeAggressive()
    {
        isAggressive = true; 
        Debug.Log("Enemy saldırgan hale geçti!");
    }

    private void Die()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.CheckForEnemies();
        }

        Destroy(gameObject);
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