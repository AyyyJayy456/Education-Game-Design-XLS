using UnityEngine;
using System.Collections.Generic;

public class WirePuzzleManager : MonoBehaviour
{
    public static WirePuzzleManager Instance;

    [Header("Panels")]
    public GameObject puzzlePanel;

    [Header("Nodes")]
    public List<WireNode> originNodes = new List<WireNode>();
    public List<WireNode> targetNodes = new List<WireNode>();

    private CancerReceptor currentReceptor;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        puzzlePanel.SetActive(false);
    }

    public void OpenPuzzle(CancerReceptor receptorSource)
    {
        currentReceptor = receptorSource;

        // Freeze physics & normal delta time
        Time.timeScale = 0f;

        // Reset all wires
        foreach (var node in originNodes) node.ResetWire();
        foreach (var node in targetNodes) node.ResetWire();

        puzzlePanel.SetActive(true);
    }

    public void CheckCompletion()
    {
        foreach (var node in originNodes)
        {
            if (!node.isConnected) return; // Still incomplete
        }

        // All nodes successfully bound
        OnPuzzleComplete();
    }

    private void OnPuzzleComplete()
    {
        puzzlePanel.SetActive(false);

        // Resume gameplay
        Time.timeScale = 1f;

        if (currentReceptor != null)
        {
            currentReceptor.OnPuzzleSolved();
        }
    }
}