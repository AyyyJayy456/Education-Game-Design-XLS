using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Win/Loss Tracking")]
    [SerializeField] private int enemiesKilled = 0;
    [SerializeField] private int enemiesBreached = 0;

    [Tooltip("Target quota of enemies to neutralize to beat the level")]
    [SerializeField] private int victoryRequirement = 5;

    [Tooltip("Maximum allowed breaches before failing")]
    [SerializeField] private int failureRequirement = 3;

    [Header("HUD Text Elements")]
    [SerializeField] private TextMeshProUGUI killedText;
    [SerializeField] private TextMeshProUGUI breachedText;

    [Header("Game Over & Victory UI Panels")]
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject failurePanel;

    [Header("Spawner Reference")]
    [SerializeField] private EnemySpawning spawner;

    [Header("Results UI")]
    [SerializeField] private GameObject resultsPanel;
    [SerializeField] private TextMeshProUGUI resultsTitleText;
    [SerializeField] private TextMeshProUGUI resultsStatsText;

    private float levelTime = 0f;
    private int immunotherapyShots = 0;
    private bool levelWon = false;

    private bool isGameOver = false;

    private void Start()
    {
        // Ensure game runs at normal speed on start/restart
        Time.timeScale = 1f;

        if (spawner == null)
        {
            spawner = FindObjectOfType<EnemySpawning>();
        }

        if (victoryPanel != null) victoryPanel.SetActive(false);
        if (failurePanel != null) failurePanel.SetActive(false);
        if (resultsPanel != null) resultsPanel.SetActive(false);

        UpdateUI();
    }

    private void Update()
    {
        if (!isGameOver)
        {
            levelTime += Time.deltaTime;
        }
    }

    public void RegisterImmunotherapyShot()
    {
        if (isGameOver)
        {
            return;
        }
        immunotherapyShots++;
    }

    public void OpenResults()
    {
        if (levelWon)
        {
            victoryPanel.SetActive(false);
        }
        else
        {
            failurePanel.SetActive(false);
        }
        resultsTitleText.text = levelWon ? "LEVEL CLEARED" : "RETREAT AND REGROUP";
        resultsStatsText.text =
            $"Cells Neutralized: {enemiesKilled} / {victoryRequirement}\n" +
            $"Cells Breached: {enemiesBreached} / {failureRequirement}\n" +
            $"Time: {(int)levelTime / 60:00}:{(int)levelTime % 60:00}\n" +
            $"Immunotherapy Shots: {immunotherapyShots}";

        resultsPanel.SetActive(true);
    }

    public void CloseResults()
    {
        resultsPanel.SetActive(false);
        if (levelWon)
        {
            victoryPanel.SetActive(true);
        }
        else
        {
            failurePanel.SetActive(true);
        }
    }

    public void IncreaseKilled()
    {
        if (isGameOver) return;

        enemiesKilled++;
        UpdateUI();

        if (enemiesKilled >= victoryRequirement)
        {
            TriggerVictory();
        }
    }

    public void IncreaseBreached()
    {
        if (isGameOver) return;

        enemiesBreached++;
        UpdateUI();

        if (enemiesBreached >= failureRequirement)
        {
            TriggerFailure();
        }
    }

    private void TriggerVictory()
    {
        isGameOver = true;
        levelWon = true;

        if (spawner != null)
        {
            spawner.SetSpawning(false);
        }

        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }
        else
        {
            Debug.Log("[GameManager] Victory! Quota satisfied.");
        }

        // Pause physics and gameplay while panel is displayed
        Time.timeScale = 0f;
    }

    private void TriggerFailure()
    {
        isGameOver = true;
        levelWon = false;

        if (spawner != null)
        {
            spawner.SetSpawning(false);
        }

        if (failurePanel != null)
        {
            failurePanel.SetActive(true);
        }
        else
        {
            Debug.Log("[GameManager] Defeat! Too many cells breached. Restarting...");
            RestartLevel();
        }

        // Pause physics and gameplay while failure panel is shown
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        // Reset timeScale so the restarted level does not stay frozen
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel(string nextSceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextSceneName);
    }

    private void UpdateUI()
    {
        if (killedText != null)
            killedText.text = $"Cells Neutralized: {enemiesKilled} / {victoryRequirement}";

        if (breachedText != null)
            breachedText.text = $"Cells Breached: {enemiesBreached} / {failureRequirement}";
    }

    public void SetQuota(int num)
    {
        victoryRequirement = num;
        UpdateUI();
    }

    public void SetFailureNum(int num)
    {
        failureRequirement = num;
        UpdateUI();
    }
}