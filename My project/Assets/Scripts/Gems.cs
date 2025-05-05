using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Gems : MonoBehaviour
{
    public static bool hasPurpleGem = false; // Kovalamaca başlatacak mücevher
    public static bool hasBlueGem = false;   // Hız artıran mücevher
    public static bool hasRedGem = false;    // Hızı düşüren mücevher

    private Scene _scene;

    private void Awake()
    {
        _scene = SceneManager.GetActiveScene();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Mor Mücevher - Kovalamacayı Başlat
        if (other.CompareTag("PurpleGem"))
        {
            hasPurpleGem = true;
            Destroy(other.gameObject);
            Debug.Log("Mor mücevher alındı! Evil Twin kovalamaya başlayacak!");
            
        }

        // Mavi Mücevher - Hız Artışı
        else if (other.CompareTag("BlueGem"))
        {
            hasBlueGem = true;
            Destroy(other.gameObject);
            Debug.Log("Mavi mücevher alındı! Hız arttı!");
        }

        // Kırmızı Mücevher - Hız Azalması
        else if (other.CompareTag("RedGem"))
        {
            hasRedGem = true;
            Destroy(other.gameObject);
            Debug.Log("Kırmızı mücevher alındı! Hız azaldı!");
        }
    }

}
