using UnityEngine;

public class PlaySpellbookCollisionSound : MonoBehaviour
{
    public AudioClip collisionSound;
    private AudioSource audioSource;
    private bool hasCollided = false; 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on this GameObject");
            enabled = false; 
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Collidable") && !hasCollided && collisionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(collisionSound);
            hasCollided = true; 
        }
    }

    // reset flag if object moves and collides again
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Collidable"))
        {
            hasCollided = false; 
        }
    }
}
