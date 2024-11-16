using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float damage = 20;
    public float range = 2f;

    public void Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                // Düşmana hasar ver
                //hit.collider.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }
}
