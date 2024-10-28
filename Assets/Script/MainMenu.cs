using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Fungsi ini akan dijalankan ketika tombol "Play" ditekan
    public void PlayGame()
    {
        // Memuat scene game utama, misalnya dengan indeks 1 (pastikan urutan scene benar di Build Settings)
        SceneManager.LoadScene("Tutorial");
    }

    // Fungsi ini akan dijalankan ketika tombol "Exit" ditekan
    public void ExitGame()
    {
        // Keluar dari aplikasi
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Untuk editor Unity
#else
            Application.Quit(); // Untuk build final
#endif
    }
}
