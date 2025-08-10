using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance;

    [Header("UI")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject gameUIPanel;
    [SerializeField] private GameObject endGamePanel;

    [Header("Timer")]
    [SerializeField] private TimerManager gameTimer;

    [Header("Player")]
    [SerializeField] private PlayerController playerController;

    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private ScoreManager scoreManager;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Time.timeScale = 0f;
        playerController.enabled = false;

        mainMenuPanel.SetActive(true);
        gameUIPanel.SetActive(false);
        endGamePanel.SetActive(false);
    }

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        gameUIPanel.SetActive(true);
        endGamePanel.SetActive(false);

        playerController.ResetPlayer();
        playerController.enabled = true;

        spawnManager.StartSpawning();
        gameTimer.StartTimer();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        mainMenuPanel.SetActive(false);
        endGamePanel.SetActive(false);
        gameUIPanel.SetActive(true);

        scoreManager.ResetScore();
        spawnManager.StartSpawning();

        playerController.ResetPlayer();
        playerController.enabled = true;

        gameTimer.StartTimer();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 0f;
        mainMenuPanel.SetActive(true);
        endGamePanel.SetActive(false);
        gameUIPanel.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
