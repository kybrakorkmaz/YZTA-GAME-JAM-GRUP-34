using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NextScenes : MonoBehaviour
{


   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Oyuncu engelle çarptığında sahneyi yeniden başlat
            SceneManager.LoadScene("GameOver");
        }
    }
}
