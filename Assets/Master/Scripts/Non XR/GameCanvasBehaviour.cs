using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvasBehaviour : MonoBehaviour
{
    [SerializeField] private Button startButton; // Reference to the start button
    [SerializeField] private TextMeshProUGUI timerText; // Reference to the timer text
    [SerializeField] private float timerLength = 10f; // Length of the game in seconds
    private float timer; // Timer for the game

    void Start()
    {
        // Set the start button's onClick event to start the game
        startButton.onClick.AddListener(StartGame);
        timer = timerLength; // Initialize timer to the full length
    }

    void Update()
    {
        if (GameManager.Instance.IsPlaying)
            Timer();
    }

    private void Timer()
    {
        // Decrease the timer
        timer -= Time.deltaTime;

        // Update the timer text
        timerText.text = "Time: " + timer.ToString("0.0");

        // Check if the timer has run out
        if (timer <= 0)
        {
            // End the game
            GameManager.Instance.EndGame();
            timerText.text = "Game Over";
            startButton.gameObject.SetActive(true); // Show the start button
            timer = timerLength; // Reset the timer for the next game
        }
    }

    public void StartGame()
    {
        // Start the game
        GameManager.Instance.StartGame();
        startButton.gameObject.SetActive(false); // Hide the start button
        timer = timerLength; // Reset the timer when the game starts
    }
}
