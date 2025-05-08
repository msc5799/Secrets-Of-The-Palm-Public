using UnityEngine;

public class Destructable : MonoBehaviour
{
    private bool collided;
    public AudioClip collide_sound;
    public AudioSource audioSource;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile" && !collided)
        {
            collided = true;
            audioSource.PlayOneShot(collide_sound);
            Destroy(gameObject);
        }
    }
}
