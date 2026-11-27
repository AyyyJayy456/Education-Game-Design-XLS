using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AntigenPuzzleUI : MonoBehaviour
{
    public static AntigenPuzzleUI Instance;

    [Header("UI Panels")]
    public GameObject puzzlePanel;
    public TextMeshProUGUI promptText;

    [Header("Antigen Matching Buttons")]
    public Button[] scfvButtons; // e.g., 3 buttons for CD19, BCMA, HER2
    public string[] scfvAntigenTypes = { "CD19", "BCMA", "HER2" };

    private CancerReceptor currentReceptor;
    private string requiredAntigen;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        puzzlePanel.SetActive(false);
    }

    public void OpenPuzzle(CancerReceptor receptor, string targetAntigen)
    {
        currentReceptor = receptor;
        requiredAntigen = targetAntigen;

        // Pause gameplay
        Time.timeScale = 0f;

        promptText.text = $"TARGET ANTIGEN DETECTED: <color=yellow>{requiredAntigen}</color>\nSelect matching CAR scFv domain:";
        puzzlePanel.SetActive(true);

        // Bind button events
        for (int i = 0; i < scfvButtons.Length; i++)
        {
            int index = i;
            scfvButtons[i].onClick.RemoveAllListeners();
            scfvButtons[i].onClick.AddListener(() => SelectAntigenOption(scfvAntigenTypes[index]));
        }
    }

    public void SelectAntigenOption(string selectedAntigen)
    {
        if (selectedAntigen == requiredAntigen)
        {
            // Correct match
            ResolvePuzzle(true);
        }
        else
        {
            // Mismatch penalty (e.g., target escape / screen shake)
            Debug.Log("Antigen mismatch! Docking failed.");
            ResolvePuzzle(false);
        }
    }

    private void ResolvePuzzle(bool success)
    {
        puzzlePanel.SetActive(false);

        // Resume gameplay
        Time.timeScale = 1f;

        if (success && currentReceptor != null)
        {
            currentReceptor.OnPuzzleSolved();
        }
    }
}