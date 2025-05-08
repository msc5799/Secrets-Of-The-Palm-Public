using UnityEngine;
using System.Collections;
using UnityEngine.Apple;
using static UnityEngine.GraphicsBuffer;

public class PullScriptLeap : MonoBehaviour
{
    //How fast the object will move
    public float moveSpeed = 1;
    //What the object will move toward
    public GameObject pullPoint;
    //What object is being moved
    public GameObject pullTarget;

    //Spell available
    private bool isEnabled = false;
    //Destructables required to enable spell
    public GameObject[] destructables;
    //Bools for script logic
    private bool allDestroysHit;
    private bool actionPlayed = false;


    public DoorController doorController; // reference to DoorController to trigger door animation (level completion)
    public NextLevel nextLevel; // reference to NextLevel to set the cube active

    public SpawnSpellbooks spellbookSpawner;
    public TextScroller textScroller; 

    public void PoseDetected()
    {
        if (isEnabled)
        {
            var step = moveSpeed * Time.deltaTime;
            pullPoint.transform.position = Vector3.MoveTowards(pullPoint.transform.position, pullTarget.transform.position, step);

        }
    }

    public void ButtonPressed()
    {

        if (doorController != null)
        {
            doorController.PlayOpenDoorAnimation();
            nextLevel.SetCubeActive();
        }
        else
        {
            UnityEngine.Debug.LogError("DoorController reference is not set!");
        }

    }

    void Update()
    {
        allDestroysHit = true;
        foreach (GameObject target in destructables)
        {
            if (target != null)
            {
                allDestroysHit = false;
            }

        }

        CheckDestroyedObjects();

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            textScroller.StartPullDialogue();
        }
    }

    void CheckDestroyedObjects()
    {
        if (allDestroysHit && !actionPlayed)
        {
            isEnabled = true;
            spellbookSpawner.SpawnPullBook();
            textScroller.StartPullDialogue();
            actionPlayed = true;
        }
    }

}
