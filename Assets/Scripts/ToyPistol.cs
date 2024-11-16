using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using TMPro;

public class ToyPistol : MonoBehaviour
{
    [Header("Gun Settings")]
    public float fireRate = 0.5f; // Atış hızı (saniye cinsinden)
    public int maxAmmo = 12; // Maksimum mermi sayısı

    [Header("References")]
    public Transform firePoint; // Merminin çıkış noktası
    public GameObject bulletPrefab; // Kullanılacak mermi prefab'ı
    public ParticleSystem muzzleFlash; // Namlu patlaması efekti
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
        // Sol tık (fare) ile ateş et
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Fire();
        }

        // R tuşu ile yeniden doldur
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
        // Merminiz yoksa ateş etmeyin
        if (currentAmmo <= 0)
        {
            Debug.Log("No ammo!");
            outofammoSound.Play();
            return;
        }

        // Atış işlemi
        nextFireTime = Time.time + fireRate; // Atış hızına göre bir sonraki atış zamanı
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

        // Mermiyi oluştur
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

    }

    void Reload()
    {
        Debug.Log("Reloading...");
        currentAmmo = maxAmmo; // Mermiyi doldur
        reloaded = true;
    }
}
