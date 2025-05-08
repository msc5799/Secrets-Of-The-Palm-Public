using UnityEngine;
using UnityEngine.UI;

public class AudioVolumeContro : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource; 

    void Start()
    {
        // ensure the slider and audio source are assigned in the inspector
        if (volumeSlider == null || audioSource == null)
        {
            Debug.LogError("Volume Slider or Audio source not assigned.");
            return;
        }

        // initialize the slider's value to the current audio volume
        volumeSlider.value = audioSource.volume;

        // add a listener to the slider's value change event
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    void OnVolumeChanged(float volume)
    {
        // set the audio source's volume to the slider's value
        audioSource.volume = volume; 
    }

    
}
