using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSC : MonoBehaviour
{
    public AudioSource footstepSound;
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && PauseSC.gamePaused == false || Input.GetKey(KeyCode.A) && PauseSC.gamePaused == false || Input.GetKey(KeyCode.S) && PauseSC.gamePaused == false || Input.GetKey(KeyCode.D) && PauseSC.gamePaused == false)
        {
            footstepSound.enabled = true;
        }
        else
        {
            footstepSound.enabled = false;
        }
    }
}
