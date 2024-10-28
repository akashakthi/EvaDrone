using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRain : MonoBehaviour
{
    public GameObject objectPrefab;
    public GameObject[] targetObjects;
    public float speed = 5f;
    public AudioClip spawnAudioClip;
    public AudioSource audioSource;
    public GameObject effectDestroy;
    public GameObject spawnPoint;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            GameObject spawnedObject = Instantiate(objectPrefab, spawnPoint.transform.position, Quaternion.identity);
            spawnedObject.transform.rotation = Quaternion.Euler(0f, 0f, 45);

            if (spawnedObject.GetComponent<Collider>() == null)
            {
                var collider = spawnedObject.AddComponent<BoxCollider>();
                collider.isTrigger = true;  // Set to trigger
            }

            var destroyOnPlayerTrigger = spawnedObject.AddComponent<DestroyOnPlayersTriggerBullet>();
            destroyOnPlayerTrigger.bulletAudioSource = audioSource;
            destroyOnPlayerTrigger.effectDestroy = effectDestroy;

            GameObject randomTarget = targetObjects[Random.Range(0, targetObjects.Length)];
            StartCoroutine(MoveToTarget(spawnedObject, randomTarget));

            PlaySpawnAudio();
            yield return new WaitForSeconds(Random.Range(4f, 6f));
        }
    }

    void PlaySpawnAudio()
    {
        if (spawnAudioClip != null)
        {
            audioSource.PlayOneShot(spawnAudioClip);
        }
    }

    IEnumerator MoveToTarget(GameObject obj, GameObject target)
    {
        while (obj != null)
        {
            Vector3 targetPosition = target.transform.position;
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, targetPosition, speed * Time.deltaTime);

            if (Vector3.Distance(obj.transform.position, targetPosition) < 0.1f)
            {
                Destroy(obj);
                yield break;
            }

            yield return null;
        }
    }
}

// Script untuk menghancurkan object ketika bertabrakan dengan Player menggunakan Collision
public class DestroyOnPlayersTriggerBullet : MonoBehaviour
{
    public AudioSource bulletAudioSource;
    public GameObject effectDestroy;
    public AudioClip explosionBulletAudioClip;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Bertabrakan dengan: {other.gameObject.name}");

        if (other.gameObject.CompareTag("Player"))
        {
            if (bulletAudioSource != null && bulletAudioSource.isPlaying)
            {
                bulletAudioSource.Stop();
            }

            if (effectDestroy != null)
            {
                Instantiate(effectDestroy, transform.position, Quaternion.identity);
                Debug.Log("Efek kehancuran berhasil di-instantiate!");
            }
            else
            {
                Debug.LogWarning("effectDestroy belum diisi di Inspector!");
            }

            if (explosionBulletAudioClip != null && bulletAudioSource != null)
            {
                bulletAudioSource.PlayOneShot(explosionBulletAudioClip);
            }

            Destroy(gameObject);
            Debug.Log("Object dihancurkan setelah bertabrakan dengan Player!");
        }
    }
}
