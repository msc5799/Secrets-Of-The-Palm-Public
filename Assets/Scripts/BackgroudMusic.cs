using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
		// add an Audio Source component to an object in the scene 
		//then add this script to make it play the audio
		
		//retrieve the information from the <component>
        audioSource = GetComponent<AudioSource>();
		
		//play the audio retriueved from the component
		audioSource.Play();
    }

    private void Update()
    {
            
 
    }
}