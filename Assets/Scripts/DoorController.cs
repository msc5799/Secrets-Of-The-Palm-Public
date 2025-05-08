using UnityEngine;
using UnityEngine.Video;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;
    public VideoPlayer videoPlayer;

    void Start()
    {
        if (doorAnimator == null)
        {
            Debug.LogError("Animator component not found");
        }

    }

    // function to call in appropriate script to trigger LevelComplete after last task is finished
    public void PlayOpenDoorAnimation()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Play();
        }
        else
        {
            Debug.LogError("Video Player not assigned");
        }

        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("LevelComplete");
            Debug.Log("LevelComplete trigger activated");
        }
        else
        {
            Debug.LogError("Animator not assigned");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            PlayOpenDoorAnimation();
        }
    }
}
