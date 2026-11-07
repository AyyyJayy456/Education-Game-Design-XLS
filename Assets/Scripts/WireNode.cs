using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WireNode : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [Header("Configuration")]
    public string antigenId = "CD19"; // Identifier to match (e.g., CD19, BCMA, HER2)
    public bool isOrigin = true;       // True for left nodes (drag source), False for right nodes (targets)
    public Color wireColor = Color.cyan;

    [Header("Components")]
    public Image nodeIcon;
    public RectTransform wireBody;     // Stretchy UI Image child representing the wire line

    [HideInInspector] public bool isConnected = false;
    private Canvas rootCanvas;
    private RectTransform rectTransform;
    private Vector2 startAnchoredPos;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rootCanvas = GetComponentInParent<Canvas>();

        if (nodeIcon != null)
            nodeIcon.color = wireColor;

        if (wireBody != null)
        {
            wireBody.gameObject.SetActive(false);
            Image wireImage = wireBody.GetComponent<Image>();
            if (wireImage != null) wireImage.color = wireColor;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isOrigin || isConnected) return;

        if (wireBody != null)
        {
            wireBody.gameObject.SetActive(true);
            wireBody.sizeDelta = new Vector2(0f, wireBody.sizeDelta.y); // Reset length
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isOrigin || isConnected || wireBody == null) return;

        // Convert mouse screen position to local canvas position
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint
        );

        UpdateWireTransform(localPoint);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isOrigin || isConnected) return;

        // Reset if released into empty space
        if (wireBody != null)
        {
            wireBody.sizeDelta = new Vector2(0f, wireBody.sizeDelta.y);
            wireBody.gameObject.SetActive(false);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        // Executes on the TARGET node when the dragged item is dropped over it
        if (isOrigin || isConnected) return;

        WireNode draggedNode = eventData.pointerDrag?.GetComponent<WireNode>();
        if (draggedNode != null && draggedNode.isOrigin && !draggedNode.isConnected)
        {
            if (draggedNode.antigenId == this.antigenId)
            {
                // Lock connection
                SnapConnection(draggedNode);
            }
            else
            {
                Debug.Log($"Mismatched receptor: {draggedNode.antigenId} cannot bind to {this.antigenId}");
            }
        }
    }

    private void SnapConnection(WireNode originNode)
    {
        originNode.isConnected = true;
        this.isConnected = true;

        // Calculate offset vector from origin to this target
        Vector3 targetWorldPos = rectTransform.position;
        Vector3 originWorldPos = originNode.rectTransform.position;
        Vector3 localDir = originNode.transform.InverseTransformPoint(targetWorldPos);

        originNode.UpdateWireTransform(localDir);

        // Notify parent puzzle controller
        WirePuzzleManager.Instance.CheckCompletion();
    }

    public void UpdateWireTransform(Vector2 targetLocalPos)
    {
        float distance = targetLocalPos.magnitude;
        float angle = Mathf.Atan2(targetLocalPos.y, targetLocalPos.x) * Mathf.Rad2Deg;

        wireBody.sizeDelta = new Vector2(distance, wireBody.sizeDelta.y);
        wireBody.localRotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void ResetWire()
    {
        isConnected = false;
        if (wireBody != null)
        {
            wireBody.sizeDelta = new Vector2(0f, wireBody.sizeDelta.y);
            wireBody.gameObject.SetActive(false);
        }
    }
}