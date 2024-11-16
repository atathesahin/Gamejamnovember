using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using TMPro;

public class M4A1 : MonoBehaviour
{
    [Header("Gun Settings")]
    public float fireRate = 0.1f; // Atýþ hýzý (saniye cinsinden)
    public int maxAmmo = 30; // Maksimum mermi sayýsý

    [Header("References")]
    public Transform firePoint; // Merminin çýkýþ noktasý
    public GameObject bulletPrefab; // Kullanýlacak mermi prefab'ý
    public ParticleSystem muzzleFlash; // Namlu patlamasý efekti
    public AudioSource gunShotSound; // Silah sesi
    public AudioSource outofammoSound;

    private int currentAmmo;
    private float nextFireTime = 0f;

    public TMP_Text crAmmoText;
    public TMP_Text maxAmmoText;
    public GameObject reloadTextObject;

    public bool reloaded = true;

    void Start()
    {
        // Mermiyi maksimuma ayarla
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        // Sol týk (fare) ile ateþ et
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Fire();
        }

        // R tuþu ile yeniden doldur
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

        if (reloaded == true)
        {
            reloadTextObject.SetActive(false);
        }

        if (reloaded == false)
        {
            reloadTextObject.SetActive(true);
        }

        if (currentAmmo <= 0)
        {
            reloaded = false;
        }
        else
        {
            reloaded = true;
        }

        crAmmoText.text = currentAmmo.ToString();
        maxAmmoText.text = maxAmmo.ToString();
    }

    void Fire()
    {
        // Merminiz yoksa ateþ etmeyin
        if (currentAmmo <= 0)
        {
            Debug.Log("No ammo!");
            outofammoSound.Play();
            return;
        }

        // Atýþ iþlemi
        nextFireTime = Time.time + fireRate; // Atýþ hýzýna göre bir sonraki atýþ zamaný
        currentAmmo--; // Mermiyi azalt

        // Görsel efekt
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        // Ses efekti
        if (gunShotSound != null)
        {
            gunShotSound.Play();
        }

        // Mermiyi oluþtur
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

    }

    void Reload()
    {
        Debug.Log("Reloading...");
        currentAmmo = maxAmmo; // Mermiyi doldur
        reloaded = true;
    }
}
