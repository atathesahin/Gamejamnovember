using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
   public Transform firePoint; 
    public GameObject bulletPrefab;
    public float fireForce = 20f; 
    public float fireRate = 0.5f; 
    public AudioClip fireSound; 
    public float recoilAmount = 1f; 
    public float recoilRecoverySpeed = 5f;
    public Sprite weaponIcon;
    public string weaponName;
    private AudioSource audioSource;
    private Vector3 originalPosition;
    private float nextFireTime = 0f; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        originalPosition = transform.localPosition; 
    }

    void Update()
    {
        RecoverRecoil(); 
    }

    public void Fire()
    {
 
        if (Time.time < nextFireTime)
        {
            return; 
        }


        nextFireTime = Time.time + fireRate;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(firePoint.forward * fireForce, ForceMode.Impulse);
        }

        ApplyRecoil(); 
        PlayFireSound(); 
    }

    private void ApplyRecoil()
    {
        transform.localPosition -= Vector3.forward * recoilAmount; 
    }

    private void RecoverRecoil()
    {
      
        transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * recoilRecoverySpeed);
    }

    private void PlayFireSound()
    {
      
        if (fireSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(fireSound);
        }
    }
}
