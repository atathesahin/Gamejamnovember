using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireForce = 20f;
    public int damage = 10;
    public int maxAmmo = 10;
    public float reloadTime = 2f;
    public AudioClip fireSound;
    public float recoilAmount = 1f;
    public float recoilRecoverySpeed = 5f;

    private int currentAmmo;
    private bool isReloading = false;
    private AudioSource audioSource;
    private Vector3 originalPosition;

    void Start()
    {
        currentAmmo = maxAmmo;
        audioSource = GetComponent<AudioSource>();
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        RecoverRecoil();
    }

    public void Fire()
    {
        if (isReloading || currentAmmo <= 0)
        {
            return;
        }

        currentAmmo--;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(firePoint.forward * fireForce, ForceMode.Impulse);
        }
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript?.SetDamage(damage);

        ApplyRecoil();
        PlayFireSound();
    }

    public void Reload()
    {
        if (isReloading || currentAmmo == maxAmmo)
        {
            return;
        }
        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
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
    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }
}
