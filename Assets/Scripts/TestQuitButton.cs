using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestQuitButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Quit button was clicked");
    }

    // Update is called once per frame
    void Start()
    {
        Button button = GetComponent<Button>();

        if (button == null)
        {
            Debug.Log("TestQuitButton is attached to an object without a Button component");
        }
    }
}
