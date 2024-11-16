using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class M4PickUp : MonoBehaviour
{
    public GameObject pickUpTextObject;
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            pickUpTextObject.SetActive(true);
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Corr");
                PlayerHand.m4 = 1;
                Destroy(this.gameObject);
                pickUpTextObject.SetActive(false);
            }
    }

    void OnTriggerExit(Collider collision)
    {
        pickUpTextObject.SetActive(false);
    }
}
