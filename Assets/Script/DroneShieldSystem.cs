using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneShieldSystem : MonoBehaviour
{
    public int maxShield = 4;
    private int currentShields;
    public GameObject[] shieldObjects;
    public GameObject effectDestroy; // Tambahkan variabel untuk efek hancur

    void Start()
    {
        currentShields = maxShield;
        UpdateShieldVisuals();
    }

    void UpdateShieldVisuals()
    {
        for (int i = 0; i < shieldObjects.Length; i++)
        {
            shieldObjects[i].SetActive(i < currentShields);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (currentShields > 0)
            {
                currentShields--;
                UpdateShieldVisuals();

                // Tampilkan efek hancur di posisi tabrakan
                if (effectDestroy != null)
                {
                    Instantiate(effectDestroy, collision.contacts[0].point, Quaternion.identity);
                }
            }
            else
            {
                // Tampilkan efek hancur terakhir di posisi drone
                if (effectDestroy != null)
                {
                    Instantiate(effectDestroy, transform.position, Quaternion.identity);
                }

                // Matikan semua AudioSource di scene
                AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
                foreach (AudioSource audio in allAudioSources)
                {
                    audio.Stop();
                }

                // Nonaktifkan drone
                gameObject.SetActive(false);
            }
        }
    }

    public void ResetShield()
    {
        currentShields = maxShield;
        UpdateShieldVisuals();
    }
}
