using UnityEngine;
using UnityEngine.ProBuilder;

public class TargetGame : MonoBehaviour
{
    //Game Objects
    public GameObject[] targets;
    public GameObject trophy;
    public GameObject spawnpoint;

    //Audio
    public AudioClip victory_sound;
    public AudioSource audioSource;

    //Bools for script logic
    private bool allTargetsHit;
    private bool soundPlayed = false;
    void Update()
    {
        allTargetsHit = true;
        foreach (GameObject target in targets)
        {
            if (target != null)
            {
                allTargetsHit = false;
            }

        }

        PlaySound();

    }

    void PlaySound()
    {
        if (allTargetsHit && !soundPlayed)
        {
            Instantiate(trophy, spawnpoint.transform.position, Quaternion.identity);
            audioSource.PlayOneShot(victory_sound);
            //Makes only happen once
            soundPlayed = true;
        }
    }
}
