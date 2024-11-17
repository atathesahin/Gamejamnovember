using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    public CharacterController characterController;
    public float moveSpeed = 5f;
    public float lookSpeed = 2f;

    [Header("Weapon Settings")]
    public TextMeshProUGUI ammoText; 
    public TextMeshProUGUI interactText; 
    public Transform weaponHolder;

    private Vector3 moveDirection;
    private float rotationX;
    private float rotationY;
    private GameObject nearbyWeapon;
    private GameObject currentWeapon;

    void Start()
    {
        UpdateAmmoUI();
        interactText.gameObject.SetActive(false); 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        MoveCharacter();
        LookAround();
        FireWeapon();
        InteractWithWeapon();
        ReloadWeapon();
    }

    private void MoveCharacter()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed;
        moveDirection = transform.right * moveX + transform.forward * moveZ;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void LookAround()
    {
        rotationX += Input.GetAxis("Mouse X") * lookSpeed;
        rotationY = Mathf.Clamp(rotationY - Input.GetAxis("Mouse Y") * lookSpeed, -90f, 90f);
        transform.localRotation = Quaternion.Euler(0, rotationX, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(rotationY, 0, 0);
    }

    private void FireWeapon()
    {
        if (Input.GetButtonDown("Fire1") && currentWeapon != null && currentWeapon.activeSelf)
        {
            Weapon weaponScript = currentWeapon.GetComponent<Weapon>();
            weaponScript?.Fire();
            UpdateAmmoUI();
        }
    }

    private void ReloadWeapon()
    {
        if (Input.GetKeyDown(KeyCode.R) && currentWeapon != null && currentWeapon.activeSelf)
        {
            Weapon weaponScript = currentWeapon.GetComponent<Weapon>();
            weaponScript?.Reload();
            UpdateAmmoUI();
        }
    }

    private void UpdateAmmoUI()
    {
        if (currentWeapon != null)
        {
            Weapon weaponScript = currentWeapon.GetComponent<Weapon>();
            if (weaponScript != null)
            {
                ammoText.text = ": " + weaponScript.GetCurrentAmmo() + "/" + weaponScript.maxAmmo;
            }
        }
        
    }

    private void InteractWithWeapon()
    {
        if (nearbyWeapon != null)
        {
            interactText.gameObject.SetActive(true);
            interactText.text = "E Basarak Silahı Al";
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickupWeapon(nearbyWeapon);
                interactText.gameObject.SetActive(false); 
            }
        }
        else
        {
            interactText.gameObject.SetActive(false); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            nearbyWeapon = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            nearbyWeapon = null;
        }
    }

    private void PickupWeapon(GameObject weapon)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon); 
        }

        currentWeapon = Instantiate(weapon.GetComponent<WeaponPickup>().weaponPrefab, weaponHolder); 
        currentWeapon.transform.localPosition = Vector3.zero; 
        currentWeapon.transform.localRotation = Quaternion.identity; 
        currentWeapon.transform.localScale = Vector3.one; 
        UpdateAmmoUI();
    }
}

