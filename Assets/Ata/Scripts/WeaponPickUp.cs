using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weaponPrefab; 
  
    public TextMeshProUGUI interactText; 
    public Transform weaponHolder;
    private GameObject currentWeapon;
    private bool isPlayerNearby = false;

    void Start()
    {
        interactText.gameObject.SetActive(false); 
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            PickupWeapon();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            interactText.gameObject.SetActive(true); 
            interactText.text = "Press E";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            interactText.gameObject.SetActive(false); 
        }
    }

    private void PickupWeapon()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon); 
        }

        currentWeapon = Instantiate(weaponPrefab, weaponHolder);
        currentWeapon.transform.localPosition = Vector3.zero; 
        currentWeapon.transform.localRotation = Quaternion.identity; 
       

        interactText.gameObject.SetActive(false); 
    }
}