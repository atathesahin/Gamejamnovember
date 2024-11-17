using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weaponPrefab;
    public TextMeshProUGUI interactText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.SetNearbyWeapon(gameObject);
                interactText.gameObject.SetActive(true); // Interact UI'yi göster
                
                // Player'ı görünür yap
                Renderer playerRenderer = other.GetComponent<Renderer>();
                if (playerRenderer != null)
                {
                    playerRenderer.enabled = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player playerScript = other.GetComponent<Player>();
            if (playerScript != null)
            {
                playerScript.SetNearbyWeapon(null);
                interactText.gameObject.SetActive(false); // Interact UI'yi gizle

                // Player'ı tekrar görünmez yap
                Renderer playerRenderer = other.GetComponent<Renderer>();
                if (playerRenderer != null)
                {
                    playerRenderer.enabled = false;
                }
            }
        }
    }
}