using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Fusion;
public class GameManager : NetworkBehaviour
{
    public NetworkObject PlayerPrefab;
    public float SpawnRadius = 3f;

    public override void Spawned()
    {
        var randomPositionOffset = Random.insideUnitCircle * SpawnRadius;
        var spawnPosition = transform.position + new Vector3(randomPositionOffset.x, transform.position.y, randomPositionOffset.y);

        Runner.Spawn(PlayerPrefab, spawnPosition, Quaternion.identity, Runner.LocalPlayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, SpawnRadius);
    }
    public static GameManager Instance; // Singleton instance

    public int currentScore = 0; // Variable to track the player's points
    public TextMeshProUGUI scoreText; // Reference to a UI Text element to display score
    [Header("Audio")]
    public AudioSource bgMusic; // Reference to a UI Audio element to play music
    public Button audioButton; // Reference to a UI Button element to play music    
    public Sprite musicPlaySprite; // Reference to a UI Sprite element to play music
    public Sprite musicPauseSprite; // Reference to a UI Sprite element to pause music
    private bool isPlaying = false; // Variable to track whether the game is currently in progress
    public bool IsPlaying { get { return isPlaying; } } // Property to return whether the game is currently in progress
    public delegate void GameMode(bool isPlaying);
    public event GameMode OnGameModeChanged;

    private void Awake()
    {
        // Ensure this is the only instance of the GameManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    private void OnEnable()
    {
        // Play the background music
        audioButton.onClick.AddListener(OnAudioClicked);
    }

    private void OnAudioClicked()
    {
        if (bgMusic.isPlaying)
        {
            bgMusic.Pause();
            audioButton.GetComponent<Image>().sprite = musicPauseSprite;
        }
        else
        {
            bgMusic.Play();
            audioButton.GetComponent<Image>().sprite = musicPlaySprite;
        }
    }

    public void StartGame()
    {
        isPlaying = true;
        Cursor.lockState = CursorLockMode.Locked;
        OnGameModeChanged?.Invoke(true);
    }

    public void EndGame()
    {
        isPlaying = false;
        Cursor.lockState = CursorLockMode.None;
        OnGameModeChanged?.Invoke(false);
    }

    // Method to add points
    public void AddPoints(int points)
    {
        currentScore += points;
        UpdateScoreUI();
    }

    // Method to reset points (if needed)
    public void ResetPoints()
    {
        currentScore = 0;
        UpdateScoreUI();
    }

    // Update the UI Text with the current score
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString();
        }
    }

    private void OnDisable()
    {
        audioButton.onClick.RemoveListener(OnAudioClicked);
    }
}
