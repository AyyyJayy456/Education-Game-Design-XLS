using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int enemiesBreached;
    private int enemiesKilled;

    [SerializeField] int victoryRequirement;
    [SerializeField] int failureRequirement;

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
        print("An enemy breached");
    }

    public void IncreaseKilled()
    {
        enemiesKilled++;
        print("An enemy died");
    }
}
