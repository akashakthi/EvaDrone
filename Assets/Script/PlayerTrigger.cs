using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{

    private AudioSource audioSource; // Referensi ke AudioSource

    // Mengambil komponen AudioSource pada saat inisialisasi
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Fungsi ini dipanggil saat object bertemu dengan trigger lain
    private void OnTriggerEnter(Collider other)
    {
        // Debug log untuk memeriksa objek yang bersentuhan
        Debug.Log($"Bersentuhan dengan: {other.gameObject.name}");

        // Cek apakah object yang bersentuhan memiliki tag "Player"
        if (other.CompareTag("Player"))
        {
            // Hentikan musik jika ada AudioSource
            if (audioSource != null)
            {
                audioSource.Stop();
                Debug.Log("Musik dihentikan saat objek dihancurkan.");
            }

            // Hancurkan object ini
            Destroy(gameObject);
            Debug.Log("Object dihancurkan setelah bertemu dengan Player!");
        }
    }
}
