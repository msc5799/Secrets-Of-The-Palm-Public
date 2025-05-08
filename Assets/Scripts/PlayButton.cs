using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    private bool hasCollided = false; // ensure fade-out only happens on first collision
    private float fadeOutDuration = 2f; // duration of fade to black; scene loads after this duration
    private Light[] menuLights; // array to hold menu lights
    public Image fadeOutPanel; // reference to image component of fade out panel (black screen)

    private void Start()
    {
        GameObject[] lights = GameObject.FindGameObjectsWithTag("MenuLight"); // find all GameObjects tagged with "MenuLight"
        menuLights = new Light[lights.Length]; // initialize menuLights array with correct lenght (number of tagged lights found)

        // Populate the menuLights array with the Light components found in the scene (tagged with "MenuLight")
        for (int i = 0; i < lights.Length; i++)
        {
            menuLights[i] = lights[i].GetComponent<Light>();
        }

        // for debugging
        if (fadeOutPanel == null)
        {
            Debug.LogError("Image reference not assigned in inspector");
        }
    }

    // for testing purposes
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(FadeAndLoadScene());
        }
    }

    public void OnButtonPress()
    {
        // checks if player's hand has collided with play button; starts coroutine if true and the logic ensures this only happens on the first collision
        hasCollided = true;
        StartCoroutine(FadeAndLoadScene());

    }

    // performs the fade-out effect then loads the new scene
    // coroutine used to ensure smooth fade effect
    private IEnumerator FadeAndLoadScene()
    {
        // check that the necessary references are assigned
        if (menuLights != null && menuLights.Length > 0 && fadeOutPanel != null)
        {
            float startTime = Time.time; // store the starting time of the fade
            float currentTime = startTime; // initialize the current time
            float[] lightIntensity = new float[menuLights.Length]; // array to store the initial intensity values

            Color startColor = fadeOutPanel.color; // store the starting color (should be transparent black)
            Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f); // target color panel should be after loop (opaque black)
            
            // store the initial intensity of each light
            for (int i = 0; i < menuLights.Length; i++)
            {
                if (menuLights[i] != null)
                {
                    lightIntensity[i] = menuLights[i].intensity; 
                }
                else
                {
                    lightIntensity[i] = 0f; 
                }
            }

            // performs the fade-out effect over time
            while (currentTime < startTime + fadeOutDuration)
            {
                float normalizedTime = (currentTime - startTime) / fadeOutDuration; // calculate the normalized time (0 to 1)

                // fade out each light in the scene
                for (int i = 0; i < menuLights.Length; i++)
                {
                    if (menuLights[i] != null)
                    {
                        float newIntensity = Mathf.Lerp(lightIntensity[i], 0f, normalizedTime); // linearly diminish the intensity
                        menuLights[i].intensity = newIntensity; 
                    }
                }

                // fade in the black overlay panel
                float newAlpha = Mathf.Lerp(startColor.a, targetColor.a, normalizedTime);
                fadeOutPanel.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha); // set the new panel color

                yield return null;
                currentTime = Time.time; 
            }

            // ensure the lights are completely off after the while loop 
            for (int i = 0; i < menuLights.Length; i++)
            {
                if (menuLights[i] != null)
                {
                    menuLights[i].intensity = 0f; 
                }
            }

            fadeOutPanel.color = targetColor; // ensure panel is fully black after loop concludes
            
        }
        else
        {
            if (menuLights == null || menuLights.Length == 0)
            {
                Debug.LogError("No main menu lights found in setup");
            }
            if (fadeOutPanel == null)
            {
                Debug.LogError("Fade out panel image not assigned");
            }
        }

        SceneManager.LoadScene("DemoLevel");
    }
}
