using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void OnButtonPress()
    {
        // if in Unity editor
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Quit button activated. Exiting playmode");

        // standalone application
        /**
        Application.Quit();
        Debug.Log("Quit button activated, quitting application");
        **/
    }
}
