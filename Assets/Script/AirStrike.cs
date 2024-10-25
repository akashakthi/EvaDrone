using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class AirStrike : MonoBehaviour
{
    public GameObject objectPrefab; // Prefab dari GameObject untuk di-instantiate
    public GameObject[] targetObjects; // Array dari GameObject sebagai titik tujuan
    public float speed = 5f; // Kecepatan bergerak
    public AudioClip spawnAudioClip; // Audio clip yang akan diputar saat spawn
    private AudioSource audioSource; // AudioSource untuk memutar audio
    public GameObject effectDestroy; // Effect yang akan ditampilkan saat objek dihancurkan


    void Start()
    {
        // Mendapatkan komponen AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();

        // Mulai Coroutine untuk secara otomatis instantiate object
        StartCoroutine(SpawnObjects());


       // effectDestroy.SetActive(false);
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            // Instantiate prefab di posisi awal (sesuaikan dengan keinginan)
            GameObject spawnedObject = Instantiate(objectPrefab, new Vector3(13.82f, -9.35f, 0), Quaternion.identity);

            // Set rotasi pada spawnedObject (X = 180, Z = -135)
            spawnedObject.transform.rotation = Quaternion.Euler(180f, 0f, -135f);

            // Tambahkan komponen Collider jika belum ada, dan set sebagai Trigger
            Collider collider = spawnedObject.GetComponent<Collider>();
            if (collider == null)
            {
                collider = spawnedObject.AddComponent<BoxCollider>(); // Menambahkan Collider jika tidak ada
            }
            collider.isTrigger = true; // Atur Collider sebagai trigger

            // Tambahkan script DestroyOnPlayerTrigger ke object yang baru di-instantiate
            var destroyOnPlayerTrigger = spawnedObject.AddComponent<DestroyOnPlayerTrigger>();
            destroyOnPlayerTrigger.airStrikeAudioSource = audioSource; // Kirim AudioSource ke DestroyOnPlayerTrigger
            destroyOnPlayerTrigger.effectDestroy = effectDestroy; // Kirim effectDestroy ke DestroyOnPlayerTrigger

            // Pilih target secara acak dari array targetObjects
            GameObject randomTarget = targetObjects[Random.Range(0, targetObjects.Length)];

            // Pindahkan object yang baru di-instantiate ke target yang dipilih
            StartCoroutine(MoveToTarget(spawnedObject, randomTarget));

            // Panggil audio saat objek di-spawn
            PlaySpawnAudio();

            // Tunggu 4 hingga 6 detik sebelum instantiate object lagi
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

// Script untuk menghancurkan object ketika bertabrakan dengan Player menggunakan Trigger
public class DestroyOnPlayerTrigger : MonoBehaviour
{
    public AudioSource airStrikeAudioSource; // AudioSource dari AirStrike
    public GameObject effectDestroy; // Effect prefab yang akan di-instantiate saat objek dihancurkan
    public AudioClip explosionAudioClip; // Audio clip untuk suara ledakan
  

    // Fungsi ini dipanggil saat object bertemu dengan trigger lain
    private void OnTriggerEnter(Collider other)
    {
        // Debug log untuk memeriksa objek yang bersentuhan
        Debug.Log($"Bersentuhan dengan: {other.gameObject.name}");

        // Cek apakah object yang bersentuhan memiliki tag "Player"
        if (other.CompareTag("Player"))
        {
            // Stop the audio if it is playing
            if (airStrikeAudioSource != null && airStrikeAudioSource.isPlaying)
            {
                airStrikeAudioSource.Stop(); // Stop the audio
            }

            // Instantiate the destruction effect at the player's position
            if (effectDestroy != null)
            {
                Instantiate(effectDestroy, other.transform.position, Quaternion.identity); // Instantiate effect at player's position
            }

            // Play the explosion audio clip
            if (explosionAudioClip != null && airStrikeAudioSource != null)
            {
                airStrikeAudioSource.PlayOneShot(explosionAudioClip); // Play explosion sound
            }

            

            // Hancurkan object ini
            Destroy(gameObject);
            Debug.Log("Object dihancurkan setelah bertemu dengan Player!");
        }
    }
}


