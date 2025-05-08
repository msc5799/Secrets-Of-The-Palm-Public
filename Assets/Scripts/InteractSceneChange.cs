using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractSceneChange : MonoBehaviour
{
	// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
	// door needs a box collider with is Triogger off
	// door needs the script
	// activation object needs a colider and a rigidBody DISABLE GRAVITY
	void OnCollisionEnter(Collision collision)
    {
		// look into incrementing scene or has to be custom for each door
		var scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.buildIndex + 1);
	}
    // Update is called once per frame
    void Update()
    {
		
	}
}
