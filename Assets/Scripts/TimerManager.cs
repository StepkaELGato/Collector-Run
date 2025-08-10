using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private float gameDuration = 60f;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject gameUIPanel;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private ScoreManager scoreManager;

    private float timeRemaining;
    private bool timerRunning = false;

    public void StartTimer()
    {
        timeRemaining = gameDuration;
        timerRunning = true;
        Time.timeScale = 1f;
        playerController.enabled = true;
        scoreManager.ResetScore();
    }

    void Update()
    {
        if (!timerRunning) return;

        if (timeRemaining > 0f)
        {
            timeRemaining -= Time.unscaledDeltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            EndGame();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void ResetTimer()
    {
        timeRemaining = gameDuration;
        timerRunning = true;
    }


    private void EndGame()
    {
        timerRunning = false;
        Time.timeScale = 0f;
        playerController.enabled = false;

        endGamePanel.SetActive(true);
        gameUIPanel.SetActive(false);
        finalScoreText.text = $"Final score: {scoreManager.CurrentScore}";

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
