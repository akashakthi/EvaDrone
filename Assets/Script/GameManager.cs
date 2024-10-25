using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject playerDrone;
    private float currentScore;
    private bool isDroneAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDroneAlive && playerDrone != null)
        {
            UpdateScore();
        }
        else if (playerDrone == null)
        {
            isDroneAlive = false;
        }
    }

    private void UpdateScore()
    {
        currentScore += Time.deltaTime;
        scoreText.text = "Score: " + Mathf.Floor(currentScore);
    }

    public void ResetScore()
    {
        currentScore = 0;
        scoreText.text = "Score: 0";
    }
}
