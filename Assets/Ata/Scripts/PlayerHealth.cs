using UnityEngine;
    public class PlayerHealth : MonoBehaviour
    {
        public int maxHealth = 100;
        [SerializeField] private int currentHealth;

        void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
      

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        void Die()
        {
        
        }
    }
