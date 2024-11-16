using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSC : MonoBehaviour
{
    //public GameObject pausePanel;
    public static bool gamePaused = false;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gamePaused == false)
        {
            Time.timeScale = 0.0f;
            //pausePanel.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            gamePaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gamePaused == true)
        {
            Time .timeScale = 1.0f;
            //pausePanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            gamePaused = false;
        }
    }
}
