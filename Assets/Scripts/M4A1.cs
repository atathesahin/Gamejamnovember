using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using TMPro;

public class M4A1 : MonoBehaviour
{
    [Header("Gun Settings")]
    public float fireRate = 0.1f; // At�� h�z� (saniye cinsinden)
    public int maxAmmo = 30; // Maksimum mermi say�s�

    [Header("References")]
    public Transform firePoint; // Merminin ��k�� noktas�
    public GameObject bulletPrefab; // Kullan�lacak mermi prefab'�
    public ParticleSystem muzzleFlash; // Namlu patlamas� efekti
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
        // Sol t�k (fare) ile ate� et
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Fire();
        }

        // R tu�u ile yeniden doldur
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
        // Merminiz yoksa ate� etmeyin
        if (currentAmmo <= 0)
        {
            Debug.Log("No ammo!");
            outofammoSound.Play();
            return;
        }

        // At�� i�lemi
        nextFireTime = Time.time + fireRate; // At�� h�z�na g�re bir sonraki at�� zaman�
        currentAmmo--; // Mermiyi azalt

        // G�rsel efekt
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        // Ses efekti
        if (gunShotSound != null)
        {
            gunShotSound.Play();
        }

        // Mermiyi olu�tur
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

    }

    void Reload()
    {
        Debug.Log("Reloading...");
        currentAmmo = maxAmmo; // Mermiyi doldur
        reloaded = true;
    }
}
