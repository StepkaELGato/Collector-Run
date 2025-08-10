using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int CurrentScore { get; private set; } = 0;

    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        UpdateScoreUI();
    }

    public void AddPoints(int amount)
    {
        CurrentScore += amount;
        UpdateScoreUI();
        Debug.Log("Счёт: " + CurrentScore);
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"{CurrentScore}";
        }
    }
}
