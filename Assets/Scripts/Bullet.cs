using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float hiz = 10f;
    public float damage = 25;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.velocity = transform.forward * hiz;
        rb.AddForce(transform.forward * hiz, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        //ENEMY TAG
    }
}
