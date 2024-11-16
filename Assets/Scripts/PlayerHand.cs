using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHand : MonoBehaviour
{
    int currentSlot = 0;

    public GameObject Pistol;
    public GameObject M4A1;

    public Image slot1, slot2, slot3;

    public static int pistol;
    public static int m4;
    public static int melee;

    void Start()
    {
        pistol = 0;
        m4 = 0;
        melee = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSlot == 0)
        {
            Pistol.SetActive(false);
            M4A1.SetActive(false);
            slot3.color = Color.white;
            slot2.color = Color.white;
            slot1.color = Color.white;
        }

        if (currentSlot == 1 && pistol == 1)
        {
            Pistol.SetActive(true);
            M4A1.SetActive(false);
            slot3.color = Color.white;
            slot2.color = Color.white;
            slot1.color = Color.yellow;
        }

        if (currentSlot == 2 && m4 == 1)
        {
            M4A1.SetActive(true);
            Pistol.SetActive(false);
            slot3.color = Color.white;
            slot2.color = Color.yellow;
            slot1.color = Color.white;
        }

        if (currentSlot == 3 && melee == 1)
        {
            M4A1.SetActive(false);
            Pistol.SetActive(false);
            slot3.color = Color.yellow;
            slot2.color = Color.white;
            slot1.color = Color.white;
        }






        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            currentSlot = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && pistol == 1)
        {
            currentSlot = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && m4 == 1)
        {
            currentSlot = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && melee == 1)
        {
            currentSlot = 3;
        }
    }
}
