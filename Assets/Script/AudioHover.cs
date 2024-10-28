using UnityEngine;

public class AudioHoverClick : MonoBehaviour
{
    public AudioClip hoverSound; // Suara untuk efek hover
    public AudioClip clickSound; // Suara untuk efek klik
    private AudioSource audioSource; // Komponen AudioSource

    void Start()
    {
        // Mendapatkan komponen AudioSource dari objek ini
        audioSource = GetComponent<AudioSource>();
    }

    void OnMouseEnter()
    {
        // Memutar suara hover saat mouse memasuki area objek
        PlaySound(hoverSound);
    }

    void OnMouseExit()
    {
        // Opsional: kamu bisa menghentikan suara saat mouse keluar dari objek
        // audioSource.Stop();
    }

    void OnMouseDown()
    {
        // Memutar suara klik saat objek di-klik
        PlaySound(clickSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.clip = clip; // Set clip ke AudioSource
            audioSource.Play(); // Putar audio
        }
    }
}
