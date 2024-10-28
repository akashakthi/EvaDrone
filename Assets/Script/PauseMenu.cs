using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public PostProcessVolume postProcessVolume; // Reference to the PostProcessVolume component
    private bool isPaused = false;

    void Start()
    {
        // Pastikan post-processing dan audio dimatikan saat awal game
        if (postProcessVolume != null)
            postProcessVolume.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused); // Toggle pause menu

        // Toggle post-processing effect
        if (postProcessVolume != null)
            postProcessVolume.enabled = isPaused;

        // Pause or resume the game
        Time.timeScale = isPaused ? 0 : 1;

        // Pause or resume all audio
        AudioListener.pause = isPaused;
    }

    public void RestartGame()
    {
        // Restart the current scene
        Time.timeScale = 1; // Ensure time scale is reset
        AudioListener.pause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMainMenu()
    {
        // Load the main menu scene, assuming it has an index of 0
        Time.timeScale = 1; // Ensure time scale is reset
        AudioListener.pause = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void ResumeGame()
    {
        TogglePause(); // Simply toggle pause state to resume game
    }
}
