using UnityEngine;
using System.Collections; // Required for Coroutines
using UnityEngine.UI;
using UnityEngine.SceneManagement; // required for UI 

public class NextLevel : MonoBehaviour
{
    private float pulseSpeed = 1f;
    private float minIntensity = 1f;
    private float maxIntensity = 5f;
    private Color emissionColor;
    public Renderer cubeRenderer;

    private Material emissiveMaterial;
    private float timeOffset;

    public Image fadeOutPanel;
    private bool hasCollided; // check for the first collision
    private float fadeOutDuration = 2f;

    public GameObject nextLevelCube; 

    private void Start()
    {
        if (cubeRenderer != null)
        {
            emissiveMaterial = cubeRenderer.material;
            if (!emissiveMaterial.HasProperty("_EmissionColor"))
            {
                Debug.LogError("Material does not have an _EmissionColor property");
                enabled = false;
                return;
            }
            timeOffset = Random.value * Mathf.PI * 2f; // Initialize timeOffset for variation
            minIntensity = Mathf.Max(0f, minIntensity); // Ensure minIntensity is not negative

            emissionColor = emissiveMaterial.GetColor("_EmissionColor");

            // Start the coroutine to cycle the emission intensity
            StartCoroutine(CycleEmissionIntensity());
        }
        else
        {
            Debug.LogError("GameObject does not have a renderer component assigned to cubeRenderer in the Inspector");
            enabled = false;
        }
    }

    // for testing purposes
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(FadeOutAndLoadScene());
        }
    }

    // cycles the cube's emission intensity (glow); goes from soft to hard to imitate a pulsing glow
    private IEnumerator CycleEmissionIntensity()
    {
        while (true) // Loop forever to continuously cycle the intensity
        {
            // Calculate the raw sine value (-1 to 1)
            float rawIntensity = Mathf.Sin(Time.time * pulseSpeed + timeOffset);

            // Remap the sine value to the desired intensity range [minIntensity, maxIntensity]
            float normalizedIntensity = (rawIntensity + 1f) * 0.5f;
            float finalIntensity = Mathf.Lerp(minIntensity, maxIntensity, normalizedIntensity);

            // Apply the intensity to the desired emission color
            Color finalEmissionColor = emissionColor * Mathf.LinearToGammaSpace(finalIntensity);

            // Set the emission color of the material
            emissiveMaterial.SetColor("_EmissionColor", finalEmissionColor);

            yield return null; // Wait for the next frame
        }
    }

    // starts coroutine to fade to black on collision with cube
    private void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided)
        {
            hasCollided = true;
            StartCoroutine(FadeOutAndLoadScene());
        }
    }


    // fade-out effect; fades to black then loads the main menu
    private IEnumerator FadeOutAndLoadScene()
    {
        if (fadeOutPanel != null)
        {
            float startTime = Time.time;
            float currentTime = startTime;
            Color startColor = fadeOutPanel.color;
            Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

            while (currentTime < startTime + fadeOutDuration)
            {
                float normalizedTime = (currentTime - startTime) / fadeOutDuration;
                float newAlpha = Mathf.Lerp(startColor.a, targetColor.a, normalizedTime);
                fadeOutPanel.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

                yield return null;
                currentTime = Time.time; 
            }

            fadeOutPanel.color = targetColor; // Ensure full fade to black
            SceneManager.LoadScene("OutsideTowerScene");
        }
        else
        {
            Debug.LogError("Fade out panel image refernce not assigned");
            SceneManager.LoadScene("OutsideTowerScene");
        }
    }

    public void SetCubeActive()
    {
        nextLevelCube.SetActive(true); 
    }
}