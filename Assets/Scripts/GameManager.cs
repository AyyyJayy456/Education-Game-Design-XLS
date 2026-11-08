using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int enemiesKilled;
    private int enemiesBreached;

    [SerializeField] TextMeshProUGUI killedText;
    [SerializeField] TextMeshProUGUI breachedText;

    [SerializeField] int victoryRequirement;
    [SerializeField] int failureRequirement;

    private void Start()
    {
        killedText.text = "Cells Neutralized: " + enemiesKilled;
        breachedText.text = "Cells Breached: " + enemiesBreached;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void IncreaseKilled()
    {
        enemiesKilled++;
        killedText.text = "Cells Neutralized: " + enemiesKilled;

        if (enemiesKilled >= victoryRequirement)
        {
            // Temporary Code
            print("Victory Requirement Passed!");
        }
    }
    public void IncreaseBreached()
    {
        enemiesBreached++;
        breachedText.text = "Cells Breached: " + enemiesBreached;

        if (enemiesBreached >= failureRequirement)
        {
            // Temporary Code
            print("Failure Requirement Passed!");
        }
    }

    public void SetQuota(int num)
    {
        victoryRequirement = num;
    }
    public void SetFailureNum(int num)
    {
        failureRequirement = num;
    }

    public void ResetText()
    {
        enemiesBreached = 0;
        enemiesKilled = 0;
        breachedText.text = "Cells Breached: " + enemiesBreached;
        killedText.text = "Cells Neutralized: " + enemiesKilled;
    }
}
