using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI textComponent;
    [TextArea(2, 5)] public string[] lines;
    public float textSpeed = 0.04f;
    public UnityEvent onDialogStop;
    private int index;
    private Coroutine typingCoroutine;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip speechBlip;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        StartDialogue();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                if (typingCoroutine != null) StopCoroutine(typingCoroutine);
                textComponent.text = lines[index];
            }
        }
    }
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index])
        {
            textComponent.text += c;

            if (audioSource != null && speechBlip != null)
            {
                audioSource.PlayOneShot(speechBlip);
            }

            yield return new WaitForSeconds(textSpeed);
        }
    }
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            typingCoroutine = StartCoroutine(TypeLine());
        }
        else
        {
            onDialogStop?.Invoke();
            gameObject.SetActive(false);
        }
    }
    public void StartDialogue()
    {
        index = 0;
        textComponent.text = string.Empty;
        gameObject.SetActive(true);
        typingCoroutine = StartCoroutine(TypeLine());
    }
}