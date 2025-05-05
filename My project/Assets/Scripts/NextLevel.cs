using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    private bool hasKey = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Anahtarı alma
        if (other.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(other.gameObject);
            Debug.Log("Anahtar alındı!");
        }

        // Kapıya dokununca kontrol
        if (other.CompareTag("Player"))
        {
            if (hasKey || GameObject.FindWithTag("Key") == null) // Anahtar var mı kontrolü
            {
                if (GameObject.FindWithTag("Door")) // Kapı varsa
                {
                    SceneManager.LoadScene("Win"); // Kazanma sahnesine geçiş
                }
                else
                {
                    Debug.Log("Sahne geçiliyor...");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Bir sonraki sahneye geçiş
                }
            }
            else
            {
                Debug.Log("Anahtar yok, kapıdan geçemezsin!");
            }
        }

        // EvilTwin ve PurpleGem çarpışması
        if (other.CompareTag("EvilTwin") && Gems.hasPurpleGem)
        {
            Debug.Log("Evil Twin çarptı! Oyun bitiyor...");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                // Oyuncuyu yok etmeden önce animasyon eklemek isteyebilirsiniz
                Destroy(player);  // Oyuncuyu yok et
            }

            SceneManager.LoadScene("GameOver"); // Game Over sahnesine geçiş
        }
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Bir sonraki sahneye geçiş yap
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(1); // Mevcut sahneyi yeniden başlat
    }
}
