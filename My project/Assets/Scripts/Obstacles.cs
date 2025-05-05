using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacles : MonoBehaviour
{
    private Scene _scene;

    private void Awake()
    {
        _scene = SceneManager.GetActiveScene();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Oyuncu engelle çarptığında sahneyi yeniden başlat
            SceneManager.LoadScene(_scene.name);
        }
    }
}
