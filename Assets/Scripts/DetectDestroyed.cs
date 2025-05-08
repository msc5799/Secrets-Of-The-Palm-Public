using UnityEngine;

public class DetectDestroyed : MonoBehaviour
{
    public GameObject[] destructables;
    public GameObject pullScript;
    private int destroyedCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pullScript.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckDestroyedObjects();
    }

    void CheckDestroyedObjects()
    {
        foreach (GameObject obj in destructables)
        {
            if (obj == null)
                destroyedCount++;
        }

        if (destroyedCount == 8)
        {
            Debug.Log("all objects destroyed");
            pullScript.SetActive(true);
        }
    }
}
