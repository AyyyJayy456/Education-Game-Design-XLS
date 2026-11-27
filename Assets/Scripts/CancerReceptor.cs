using UnityEngine;

public enum ReceptorPuzzleType
{
    QuickAntigenMatch,
    WireLinking
}

public class CancerReceptor : MonoBehaviour
{
    [Header("Puzzle Settings")]
    public ReceptorPuzzleType puzzleType = ReceptorPuzzleType.QuickAntigenMatch;
    public string targetAntigenId = "CD19";

    [Header("Visuals")]
    public SpriteRenderer receptorVisual;
    public Color activeColor = Color.cyan;
    public Color neutralizedColor = Color.gray;

    private bool isNeutralized = false;

    private void Start()
    {
        if (receptorVisual != null)
            receptorVisual.color = activeColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isNeutralized) return;

        if (collision.CompareTag("PlayerBullet") || collision.gameObject.name.Contains("Bullet"))
        {
            Destroy(collision.gameObject);

            if (puzzleType == ReceptorPuzzleType.QuickAntigenMatch)
            {
                if (AntigenPuzzleUI.Instance != null)
                    AntigenPuzzleUI.Instance.OpenPuzzle(this, targetAntigenId);
            }
            else if (puzzleType == ReceptorPuzzleType.WireLinking)
            {
                if (WirePuzzleManager.Instance != null)
                    WirePuzzleManager.Instance.OpenPuzzle(this);
            }
        }
    }

    public void OnPuzzleSolved()
    {
        isNeutralized = true;

        if (receptorVisual != null)
            receptorVisual.color = neutralizedColor;

        // Turn off this collider so future bullets don't hit it again
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        // Sends the call up to ReceptorDeathManager on the parent
        SendMessageUpwards("ApplyReceptorDamage", 25f, SendMessageOptions.DontRequireReceiver);
    }
}