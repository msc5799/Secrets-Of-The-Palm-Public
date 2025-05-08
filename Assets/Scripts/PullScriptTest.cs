using UnityEngine;

public class PullScriptTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
	}
    // Update is called once per frame
    void Update()
    {
		// how fast the object will move
		float moveSpeed = 1;
		//gets position of camera
		var pos = GameObject.Find("Camera").transform.position;
		//calculates the direction to move for the object
		Vector3 directionToMove = pos - transform.position;
		// finds the next movement based on the above information
		directionToMove = directionToMove.normalized * Time.deltaTime * moveSpeed;
		float maxDistance = Vector3.Distance(transform.position, pos);
		// on input the object moves to the camera
		if (Input.GetKey("f"))
		{
			transform.position = transform.position + Vector3.ClampMagnitude(directionToMove, maxDistance);
		}
		// on input the objectmoves away from the camera
		if (Input.GetKey("b"))
		{
			transform.position = transform.position - Vector3.ClampMagnitude(directionToMove, maxDistance);
		}
	}
}
