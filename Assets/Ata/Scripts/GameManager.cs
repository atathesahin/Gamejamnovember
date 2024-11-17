using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void CheckForEnemies()
    {
        // Sahnedeki "Enemy" tag'ine sahip tüm objeleri kontrol et
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            EndGame(); // Tüm düşmanlar öldü, oyunu bitir
        }
    }

    private void EndGame()
    {
  
         SceneManager.LoadScene("Final");
    }
}
