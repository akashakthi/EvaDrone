using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerDrone;

    [Header("UI Elements")]
    public GameObject loseUI;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private bool isDroneAlive = true;
    private float currentScore = 0f;
    private float highScore = 0f;
    private float scoreIncreaseRate = 100f;

    private DroneShieldSystem droneShieldSystem;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        ResetScore();
        droneShieldSystem = playerDrone.GetComponent<DroneShieldSystem>();

        initialPosition = playerDrone.transform.position;

        highScoreText.text = "High Score " + Mathf.FloorToInt(highScore).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDroneAlive)
        {
            UpdateScore(); 
        }

        LoseConUI();
    }

    // Method untuk memperbarui skor
    private void UpdateScore()
    {
        
        currentScore += Time.deltaTime * scoreIncreaseRate;
        scoreText.text = "Score: " + Mathf.FloorToInt(currentScore).ToString();
    }

    public void ResetScore()
    {
        currentScore = 0f;
        scoreText.text = "Score: 0";
    }

    public void LoseConUI()
    {
        if (playerDrone != null && !playerDrone.activeSelf)
        {
            isDroneAlive = false; 
            loseUI.SetActive(true);
            Time.timeScale = 0f;

            if (currentScore > highScore)
            {
                highScore = currentScore;
                highScoreText.text = "High Score " + Mathf.FloorToInt(highScore).ToString();
            }
        }
    }

    public void MainMenu(string SceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneName);
    }

    public void TryAgain()
    {
        playerDrone.SetActive(true);

        playerDrone.transform.position = initialPosition;

        if(droneShieldSystem != null )
        {
            droneShieldSystem.ResetShield();
        }

        ResetScore();
        loseUI.SetActive(false);
        isDroneAlive = true;
        Time.timeScale = 1f;

    }
}
