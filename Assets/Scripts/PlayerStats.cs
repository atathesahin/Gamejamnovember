using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float health;
    public float maxHealth = 100;
    public float stamina = 100;

    public float heat;
    public float maxHeat = 100;
    public float minHeat = 0;
    public bool heating = false;

    public Image healthBar;
    public Image heatBar;
    void Start()
    {
        health = 100;
        heat = 0;
        maxHeat = 100;
        minHeat = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            health -= 25;
        }

        if (heating == false)
        {
            heat += 1 * Time.deltaTime;
        }
        else if (heating == true)
        {
            heat -= 1 * Time.deltaTime;
        }

        if (heat >= 100)
        {
            heat = maxHeat;
        }

        if (heat <= 0)
        {
            heat = minHeat;
        }

        healthBar.fillAmount = health / maxHealth;
        heatBar.fillAmount = heat / maxHeat;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "HeatZone")
        {
            heating = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "HeatZone")
        {
            heating = false;
        }
    }
}
