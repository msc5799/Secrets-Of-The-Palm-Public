using UnityEngine;
using System.Collections;
using UnityEngine.Apple;

public class SpawnBall : MonoBehaviour
{
    public Camera cam;
    public GameObject projectile;
    public Transform FirePoint;
    public float projectileSpeed = 10;
    //public Transform Spawner;

    private Vector3 destination;

    private bool fireBallSpawned = false; // Flag to track if a Fireball has been spawned the first time
    private bool canSpawnFireball = false; // Flag to unlock fireball spell

    public SpawnSpellbooks spellbookSpawner;
    public TextScroller textScroller; 

    public AudioClip fire_sound;
    private AudioSource audioSource;

    void Start()
    {
        //replace with 24 later
        StartCoroutine(UnlockFireball(24f));
        StartCoroutine(SpawnFireballBook(14f));
        audioSource = GetComponent<AudioSource>();
    }

    public void PoseDetected()
    {
        if (canSpawnFireball)
        {
            //Instantiate(SphereSpawn, Spawner.position, Quaternion.identity);
            //Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            Ray ray = new Ray(FirePoint.position, FirePoint.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
                destination = hit.point;
            else
                destination = ray.GetPoint(1000);

            audioSource.PlayOneShot(fire_sound);
            InstantiateProjectile(FirePoint);

            // Check if this is the first time a fireball has been spawned
            if (!fireBallSpawned)
            {
                fireBallSpawned = true;
                textScroller.PlayDestroyDialogue();
            }
        }
        else
        {
            Debug.Log("Fireball spell not available yet");
        }

    }

    public void InstantiateProjectile(Transform FirePoint)
    {
        var projectileObj = Instantiate(projectile, FirePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().linearVelocity = (destination - FirePoint.position).normalized * projectileSpeed;
    }

    public void EnableFireball()
    {
        canSpawnFireball = true;
        Debug.Log("Fireball spell unlocked");
    }

    IEnumerator UnlockFireball(float delay)
    {
        yield return new WaitForSeconds(delay);

        EnableFireball();

    }

    IEnumerator SpawnFireballBook(float delay)
    {
        yield return new WaitForSeconds(delay);
        spellbookSpawner.SpawnFireballBook();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            textScroller.PlayDestroyDialogue();
        }
    }
}
