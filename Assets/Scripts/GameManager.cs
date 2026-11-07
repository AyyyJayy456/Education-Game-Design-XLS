using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int enemiesBreached;
    private int enemiesKilled;

    [SerializeField] TextMeshProUGUI breachedText;
    [SerializeField] TextMeshProUGUI killedText;

    [SerializeField] int victoryRequirement;
    [SerializeField] int failureRequirement;

    private void Start()
    {
        breachedText.text = "Cells Breached: " + enemiesBreached;
        killedText.text = "Cells Neutralized: " + enemiesKilled;
    }

    // Update is called once per frame
    void Update()
    {
        DetermineOutcome();
    }

    void DetermineOutcome()
    {
        if(enemiesKilled >= victoryRequirement)
        {
            // Temporary Code
            print("Victory Requirement Passed!");
        }
        
        if(enemiesBreached >= failureRequirement)
        {
            // Temporary Code
            print("Failure Requirement Passed!");
        }
    }

    public void IncreaseBreached()
    {
        enemiesBreached++;
        breachedText.text = "Cells Breached: " + enemiesBreached;
        print("An enemy breached");
    }

    public void IncreaseKilled()
    {
        enemiesKilled++;
        killedText.text = "Cells Neutralized: " + enemiesKilled;
        print("An enemy died");
    }
}
