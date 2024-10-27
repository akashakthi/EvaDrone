using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRain : MonoBehaviour
{
    public GameObject objectPrefab; // Prefab dari GameObject untuk di-instantiate
    public GameObject[] targetObjects; // Array dari GameObject sebagai titik tujuan
    public float speed = 5f; // Kecepatan bergerak
    public AudioClip spawnAudioClip; // Audio clip yang akan diputar saat spawn
    private AudioSource audioSource; // AudioSource untuk memutar audio
    public GameObject effectDestroy; // Effect yang akan ditampilkan saat objek dihancurkan
    public GameObject spawnPoint; // Reference to the spawn point GameObject
    void Start()
    {
        // Mendapatkan komponen AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();

        // Mulai Coroutine untuk secara otomatis instantiate object
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            // Instantiate prefab at the spawnPoint's position
            GameObject spawnedObject = Instantiate(objectPrefab, spawnPoint.transform.position, Quaternion.identity);

            // Set rotation on spawnedObject (X = 0, Y = 0, Z = 45)
            spawnedObject.transform.rotation = Quaternion.Euler(0f, 0f, 45);

            // Add a Collider component if it doesn't exist and set it as a trigger
            Collider collider = spawnedObject.GetComponent<Collider>();
            if (collider == null)
            {
                collider = spawnedObject.AddComponent<BoxCollider>(); // Add Collider if none exists
            }

            // Add DestroyOnPlayerTrigger script to the newly instantiated object
            var destroyOnPlayerTrigger = spawnedObject.AddComponent<DestroyOnPlayerTrigger>();
            destroyOnPlayerTrigger.airStrikeAudioSource = audioSource; // Pass AudioSource to DestroyOnPlayerTrigger
            destroyOnPlayerTrigger.effectDestroy = effectDestroy; // Pass effectDestroy to DestroyOnPlayerTrigger

            // Choose a random target from the targetObjects array
            GameObject randomTarget = targetObjects[Random.Range(0, targetObjects.Length)];

            // Move the newly instantiated object towards the chosen target
            StartCoroutine(MoveToTarget(spawnedObject, randomTarget));

            // Play audio when the object is spawned
            PlaySpawnAudio();

            // Wait 4 to 6 seconds before instantiating another object
            float spawnInterval = Random.Range(4f, 6f);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void PlaySpawnAudio()
    {
        if (spawnAudioClip != null)
        {
            audioSource.PlayOneShot(spawnAudioClip); // Memutar audio clip
        }
    }

    IEnumerator MoveToTarget(GameObject obj, GameObject target)
    {
        // Lakukan pergerakan object ke target
        while (obj != null)
        {
            Vector3 targetPosition = target.transform.position;

            // Pindahkan object menggunakan MoveTowards
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, targetPosition, speed * Time.deltaTime);

            // Berhenti ketika mencapai target
            if (Vector3.Distance(obj.transform.position, targetPosition) < 0.1f) // Menambahkan toleransi untuk mencapai target
            {
                Debug.Log("Objek mencapai target! Menghancurkan objek.");
                Destroy(obj); // Hancurkan objek setelah mencapai target
                yield break; // Hentikan Coroutine untuk object ini
            }

            yield return null; // Tunggu sampai frame berikutnya
        }
    }
}

// Script untuk menghancurkan object ketika bertabrakan dengan Player menggunakan Collision
public class DestroyOnPlayersTriggerBullet : MonoBehaviour
{
    public AudioSource bulletAudioSource; // AudioSource dari BulletRain
    public GameObject effectBulletDestroy; // Effect prefab yang akan di-instantiate saat objek dihancurkan
    public AudioClip explosionBulletAudioClip; // Audio clip untuk suara ledakan

    // Fungsi ini dipanggil saat object bertabrakan dengan objek lain
    private void OnCollisionEnter(Collision collision)
    {
        // Debug log untuk memeriksa objek yang bertabrakan
        Debug.Log($"Bertabrakan dengan: {collision.gameObject.name}");

        // Cek apakah object yang bertabrakan memiliki tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Stop the audio if it is playing
            if (bulletAudioSource != null && bulletAudioSource.isPlaying)
            {
                bulletAudioSource.Stop(); // Stop the audio
            }

            // Instantiate the destruction effect at the player's position
            if (effectBulletDestroy != null)
            {
                Instantiate(effectBulletDestroy, collision.transform.position, Quaternion.identity); // Instantiate effect at player's position
            }

            // Play the explosion audio clip
            if (explosionBulletAudioClip != null && bulletAudioSource != null)
            {
                bulletAudioSource.PlayOneShot(explosionBulletAudioClip); // Play explosion sound
            }

            // Hancurkan object ini
            Destroy(gameObject);
            Debug.Log("Object dihancurkan setelah bertabrakan dengan Player!");
        }
    }
}
