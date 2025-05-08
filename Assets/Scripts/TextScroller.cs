using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TextScroller : MonoBehaviour
{
    public float typeSpeed = 0.05f;
    public float initialStartDelay = 2f;
    public float delayBetweenLines = 2f;
    public List<string> initialDialogueLines; // List to hold the initial dialogue
    public List<string> pullDialogueLines; // list to hold pull dialogue

    private TextMeshProUGUI textMeshPro;
    private string textToDisplay = "";
    private string currentDisplayedText = "";
    private int characterIndex = 0;
    private int currentLineIndex = 0;

    public AudioClip textScrollEffect;
    public float textScrollVolume = 0.5f;
    public AudioSource audioSource;

    public Canvas container; 

    void Start()
    {
        audioSource.playOnAwake = false; 

        textMeshPro = GetComponent<TextMeshProUGUI>();
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on this GameObject.");
            enabled = false;
            return;
        }

        // ensure the text is cleared to start
        textMeshPro.text = "";

        // Start the initial dialogue sequence with a delay
        StartCoroutine(StartInitialDialogue());

    }

    IEnumerator TypeText(string textToType)
    {
        currentDisplayedText = "";
        textMeshPro.text = "";
        characterIndex = 0;

        while (characterIndex < textToType.Length)
        {
            currentDisplayedText += textToType[characterIndex];
            textMeshPro.text = currentDisplayedText;
            characterIndex++;

            if (textScrollEffect != null && !audioSource.isPlaying)
            {
                audioSource.PlayOneShot(textScrollEffect, textScrollVolume);
            }

            yield return new WaitForSeconds(typeSpeed);
        }

        audioSource.Stop();
    }

    IEnumerator StartInitialDialogue()
    {
        yield return new WaitForSeconds(initialStartDelay);

        if (initialDialogueLines != null && initialDialogueLines.Count > 0)
        {
            while (currentLineIndex < initialDialogueLines.Count)
            {
                yield return StartCoroutine(TypeText(initialDialogueLines[currentLineIndex]));
                currentLineIndex++;
                if (currentLineIndex < initialDialogueLines.Count)
                {
                    yield return new WaitForSeconds(delayBetweenLines);
                }
            }
            // Optionally, do something after the initial dialogue is finished
            Debug.Log("Initial dialogue sequence complete.");
        }

    }

    public void PlayDestroyDialogue()
    {
        StartCoroutine(StartDestroyDialogue());
    }

    // plays new dialogue after fireball spawns the first time
    IEnumerator StartDestroyDialogue()
    {
        yield return StartCoroutine(TypeText("Good. Aim for that pile of junk over there and clear it out with your fireball spell."));
        yield return new WaitForSeconds(3f);
        SetContainerInactive(); // <-- hide dialogue box while player completes task
        ClearText();
    }

    // referenced in PullScriptLeap to start coroutine for pull dialogue when destroyedCount = 8
    public void StartPullDialogue()
    {
        StopAllCoroutines();
        currentLineIndex = 0;
        SetContainerActive();
        StartCoroutine(PlayPullDialogue());
    }

    // plays new dialogue for pull spell + task; delay of 2s between dialogue lines
    IEnumerator PlayPullDialogue()
    {
        yield return new WaitForSeconds(1f);

        if (pullDialogueLines != null && pullDialogueLines.Count > 0)
        {
            while (currentLineIndex < pullDialogueLines.Count)
            {
                yield return StartCoroutine(TypeText(pullDialogueLines[currentLineIndex]));
                currentLineIndex++;
                if (currentLineIndex < pullDialogueLines.Count)
                {
                    yield return new WaitForSeconds(delayBetweenLines);
                }
            }
        }

        yield return new WaitForSeconds(3f);
        SetContainerInactive();
        ClearText();
    }

    // Public method to start scrolling with new text 
    public void StartScrollingText(string text)
    {
        StopAllCoroutines(); // Stop any ongoing initial dialogue
        textToDisplay = text;
        StartCoroutine(TypeText(textToDisplay));
    }

    // Public method to clear the text box
    public void ClearText()
    {
        StopAllCoroutines();
        textToDisplay = "";
        currentDisplayedText = "";
        characterIndex = 0;
        currentLineIndex = 0;
        textMeshPro.text = "";
    }

    public void SetContainerActive()
    {
        container.gameObject.SetActive(true);
    }

    public void SetContainerInactive()
    {
        container.gameObject.SetActive(false);
    }
}