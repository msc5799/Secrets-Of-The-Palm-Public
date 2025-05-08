using UnityEngine;
using System;

public class MainMenuController : MonoBehaviour
{
    public Transform indexFingerTip;
    public GameObject playButton;
    public GameObject quitButton;
    public float proximityThreshold = 0.1f;
    public float activationDelay = 0.5f;

    private bool playButtonInProximity = false;
    private float playButtonProximityStartTime = 0f;

    private bool quitButtonInProximity = false;
    private float quitButtonProximityStartTime = 0f;

    private void Start()
    {
        // error checking for required references
        if (indexFingerTip == null) Debug.LogError("Index finger tip Transform not assigned");
        if (playButton == null) Debug.LogError("Play button GameObject not assigned");
        if (quitButton == null) Debug.LogError("Quit button GameObject not assigned");
        if (playButton != null && playButton.GetComponent<Collider>() == null) Debug.LogError("Play button has no collider");
        if (quitButton != null && quitButton.GetComponent<Collider>() == null) Debug.LogError("Quit button has no collider");
        if (playButton != null && playButton.GetComponent<PlayButton>() == null) Debug.LogError("PlayButton script not found on play button");
        if (quitButton != null && quitButton.GetComponent<QuitButton>() == null) Debug.LogError("QuitButton script not found on quit button");
    }

    private void Update()
    {
        if (indexFingerTip == null) return;

        // Handle play button
        if (playButton != null)
        {
            HandleButtonInteraction(playButton, ref playButtonInProximity, ref playButtonProximityStartTime, () =>
            {
                PlayButton playAction = playButton.GetComponent<PlayButton>();
                if (playAction != null)
                {
                    //playAction.LoadScene();
                }
            });
        }

        // Handle quit button
        if (quitButton != null)
        {
            HandleButtonInteraction(quitButton, ref quitButtonInProximity, ref quitButtonProximityStartTime, () =>
            {
                QuitButton quitAction = quitButton.GetComponent<QuitButton>();
                if (quitAction != null)
                {
                    //quitAction.QuitGame();
                }
            });
        }
    }

    void HandleButtonInteraction(GameObject buttonObject, ref bool inProximity, ref float proximityStartTime, System.Action onActivate)
    {
        Collider buttonCollider = buttonObject.GetComponent<Collider>();
        if (buttonCollider != null)
        {
            Vector3 closestPoint = buttonCollider.ClosestPoint(indexFingerTip.position);
            float distance = Vector3.Distance(closestPoint, indexFingerTip.position);

            if (distance <= proximityThreshold && !inProximity)
            {
                inProximity = true;
                proximityStartTime = Time.time;
            }
            else if (distance > proximityThreshold && inProximity)
            {
                inProximity = false;
                proximityStartTime = 0f;
            }

            if (inProximity && Time.time >= proximityStartTime + activationDelay)
            {
                if (onActivate != null)
                {
                    onActivate.Invoke();
                }
                inProximity = false;
                proximityStartTime = 0f;
            }
        }
        else
        {
            Debug.LogError(buttonObject.name + " has no collider");
        }
    }
}