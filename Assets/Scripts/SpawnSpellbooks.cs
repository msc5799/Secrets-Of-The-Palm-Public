using UnityEngine;

public class SpawnSpellbooks : MonoBehaviour 
{
    public GameObject fireballSpellBookPrefab;
    public GameObject pullSpellBookPrefab;

    public GameObject fireballCheckpointLocation; 
    public GameObject pullCheckpointLocation;   

    // check if object has been spawned; if true, do not spawn again
    private bool fireballBookSpawned = false;
    private bool pullBookSpawned = false;


    public void SpawnFireballBook()
    {
        if (!fireballBookSpawned)
        {
            fireballBookSpawned = true;
            Debug.Log("Spawning Fireball spell book at checkpoint.");
            // Instantiate at the checkpoint's position and rotation
            if (fireballSpellBookPrefab != null && fireballCheckpointLocation != null)
            {
                Instantiate(fireballSpellBookPrefab, fireballCheckpointLocation.transform.position, fireballCheckpointLocation.transform.rotation);
                Debug.Log("Fireball spell book instantiated at " + fireballCheckpointLocation.transform.position);
            }
            else
            {
                Debug.LogError("Fireball spell book prefab or checkpoint location not assigned!");
            }
        }
    }

    public void SpawnPullBook()
    {
        if (!pullBookSpawned)
        {
            pullBookSpawned = true;
            Debug.Log("Spawning Pull spell book at checkpoint.");
            // Instantiate at the checkpoint's position and rotation
            if (pullSpellBookPrefab != null && pullCheckpointLocation != null)
            {
                Instantiate(pullSpellBookPrefab, pullCheckpointLocation.transform.position, pullCheckpointLocation.transform.rotation);
                Debug.Log("Pull spell book instantiated at " + pullCheckpointLocation.transform.position);
            }
            else
            {
                Debug.LogError("Pull spell book prefab or checkpoint location not assigned!");
            }
        }
    }
}