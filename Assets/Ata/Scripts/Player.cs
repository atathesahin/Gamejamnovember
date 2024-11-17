using TMPro;
using UnityEngine;
using System.Collections.Generic;
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

    [Header("UI Settings")]
    public List<Image> weaponSlots; 
    public Sprite emptySlotSprite; 

    public List<GameObject> weaponInventory = new List<GameObject>();
    public int currentWeaponIndex = -1;

    private Vector3 moveDirection;
    private float rotationX;
    private float rotationY;
    public GameObject nearbyWeapon;

    void Start()
    {
        UpdateAmmoUI();
        UpdateWeaponSlots(); 
        if (interactText != null)
        {
            interactText.gameObject.SetActive(false); 
        }
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
        SwitchWeapon();
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
        if (Input.GetButtonDown("Fire1") && currentWeaponIndex != -1)
        {
            Weapon weaponScript = weaponInventory[currentWeaponIndex].GetComponent<Weapon>();
            weaponScript?.Fire();
            UpdateAmmoUI();
        }
    }

    private void ReloadWeapon()
    {
        if (Input.GetKeyDown(KeyCode.R) && currentWeaponIndex != -1)
        {
            Weapon weaponScript = weaponInventory[currentWeaponIndex].GetComponent<Weapon>();
            weaponScript?.Reload();
            UpdateAmmoUI();
        }
    }

    private void UpdateAmmoUI()
    {
        if (currentWeaponIndex != -1)
        {
            Weapon weaponScript = weaponInventory[currentWeaponIndex].GetComponent<Weapon>();
            if (weaponScript != null)
            {
                ammoText.text = ": " + weaponScript.GetCurrentAmmo() + "/" + weaponScript.maxAmmo;
            }
        }
        else
        {
            ammoText.text = "No Weapon";
        }
    }

    private void PickupWeapon(GameObject weapon)
    {
        GameObject newWeapon = Instantiate(weapon.GetComponent<WeaponPickup>().weaponPrefab, weaponHolder);
        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.transform.localRotation = Quaternion.identity;
        newWeapon.transform.localScale = Vector3.one;

        weaponInventory.Add(newWeapon);
        currentWeaponIndex = weaponInventory.Count - 1;

        Destroy(weapon.gameObject);
        UpdateAmmoUI();
        UpdateWeaponSlots(); 
    }

    private void UpdateWeaponSlots()
    {
        for (int i = 0; i < weaponSlots.Count; i++)
        {
            weaponSlots[i].sprite = emptySlotSprite; 
            weaponSlots[i].rectTransform.localPosition = Vector3.zero; 
        }


        for (int i = 0; i < weaponInventory.Count && i < weaponSlots.Count; i++)
        {
            Weapon weaponScript = weaponInventory[i].GetComponent<Weapon>();
            if (weaponScript != null)
            {
                weaponSlots[i].sprite = weaponScript.weaponIcon; 
            }

           
            if (i == currentWeaponIndex)
            {
                weaponSlots[i].rectTransform.localPosition = new Vector3(0, 10, 0); 
            }
        }
    }

    private void InteractWithWeapon()
    {
        if (nearbyWeapon != null)
        {
            interactText.gameObject.SetActive(true);
            interactText.text = "Press E to pick up weapon";
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

    private void SwitchWeapon()
    {
        if (weaponInventory.Count > 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && weaponInventory.Count >= 1)
            {
                EquipWeapon(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && weaponInventory.Count >= 2)
            {
                EquipWeapon(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && weaponInventory.Count >= 3)
            {
                EquipWeapon(2);
            }
        }
    }

    private void EquipWeapon(int index)
    {
        if (currentWeaponIndex != -1)
        {
            weaponInventory[currentWeaponIndex].SetActive(false);
        }

        currentWeaponIndex = index;
        weaponInventory[currentWeaponIndex].SetActive(true);
        UpdateAmmoUI();
    }

    public void SetNearbyWeapon(GameObject weapon)
    {
        nearbyWeapon = weapon;
    }
}

