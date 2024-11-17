using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5f;
    [SerializeField] private int damage = 10;

    public GameObject hitEffectPrefab; 

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void SetDamage(int damageAmount)
    {
        damage = damageAmount;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {

            enemy.TakeDamage(damage);


            if (hitEffectPrefab != null)
            {
                Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            }
        }

        Destroy(gameObject); 
    }
}

