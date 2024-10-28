using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Diperlukan untuk menggunakan Panel
using System.Collections;

public class LoadSceneWithPanelFade : MonoBehaviour
{
    public GameObject fadePanel; // Panel yang akan digunakan untuk efek gelap
    private bool isLoading = false; // Menandakan apakah scene sedang dimuat

    void Update()
    {
        // Memeriksa apakah pemain menekan sembarang tombol
        if (Input.anyKeyDown && !isLoading)
        {
            isLoading = true; // Tandai bahwa kita sedang memuat
            StartCoroutine(FadeToBlackAndLoadScene(3f)); // Panggil coroutine untuk menggelapkan layar dan memuat scene
        }
    }

    private IEnumerator FadeToBlackAndLoadScene(float delay)
    {
        // Menampilkan panel dengan efek gelap
        CanvasGroup canvasGroup = fadePanel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = fadePanel.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0; // Set alpha awal (transparan)
        float elapsedTime = 0f;

        // Meningkatkan alpha dari 0 ke 1 selama delay
        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / delay);
            yield return null;
        }

        // Memuat scene "Gameplay"
        SceneManager.LoadScene("Gameplays");
    }
}
