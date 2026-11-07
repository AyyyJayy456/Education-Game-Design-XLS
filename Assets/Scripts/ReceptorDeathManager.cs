using System.Collections.Generic;
using UnityEngine;

public class ReceptorDeathManager : MonoBehaviour
{
    [Header("Death Settings")]
    [SerializeField] private GameObject deathEffectPrefab;

    private List<CancerReceptor> receptors = new List<CancerReceptor>();
    private int solvedCount = 0;
    private GameManager gameManager;

    private void Start()
    {
        // Automatically find all child receptors attached to this cell
        receptors.AddRange(GetComponentsInChildren<CancerReceptor>());

        GameObject gm = GameObject.Find("GameManager");
        if (gm != null)
            gameManager = gm.GetComponent<GameManager>();
    }

    // Listens for the SendMessageUpwards call from CancerReceptor.cs
    public void ApplyReceptorDamage(float damage)
    {
        solvedCount++;
        Debug.Log($"[ReceptorDeathManager] Cleared {solvedCount} of {receptors.Count} receptors on {gameObject.name}");

        if (solvedCount >= receptors.Count)
        {
            DestroyCancerCell();
        }
    }

    private void DestroyCancerCell()
    {
        Debug.Log($"[ReceptorDeathManager] All receptors solved! Destroying {gameObject.name}");

        // Award score if GameManager exists
        if (gameManager != null)
        {
            gameManager.IncreaseKilled();
        }

        if (deathEffectPrefab != null)
        {
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        // Destroys the root cancer cell
        Destroy(gameObject);
    }
}