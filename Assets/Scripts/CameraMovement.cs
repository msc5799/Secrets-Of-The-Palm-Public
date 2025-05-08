using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		
    }

    // Update is called once per frame
	float dps = 90;
	float move = 20;
    void Update()
    {
		// move camera rotations
		if (Input.GetKey("d"))
		{
			transform.Rotate(new Vector3(0, dps, 0) * Time.deltaTime);
		}
		if (Input.GetKey("a"))
		{
			transform.Rotate(new Vector3(0, -dps, 0) * Time.deltaTime);
		}
		if (Input.GetKey("s"))
		{
			transform.Rotate(new Vector3(dps, 0, 0) * Time.deltaTime);
		}
		if (Input.GetKey("w"))
		{
			transform.Rotate(new Vector3(-dps, 0, 0) * Time.deltaTime);
		}
		// move camera position
		if (Input.GetKey("up"))
		{
			transform.Translate(new Vector3(0, 0, move) * Time.deltaTime);
		}
		if (Input.GetKey("right"))
		{
			transform.Translate(new Vector3(move, 0, 0) * Time.deltaTime);
		}
		if (Input.GetKey("down"))
		{
			transform.Translate(new Vector3(0, 0, -move) * Time.deltaTime);
		}
		if (Input.GetKey("left"))
		{
			transform.Translate(new Vector3(-move, 0, 0) * Time.deltaTime);
		}
		// roll camera
		if (Input.GetKey("q"))
		{
			transform.Rotate(new Vector3(0, 0, dps) * Time.deltaTime);
		}
		if (Input.GetKey("e"))
		{
			transform.Rotate(new Vector3(0, 0, -dps) * Time.deltaTime);
		}
	}
}
