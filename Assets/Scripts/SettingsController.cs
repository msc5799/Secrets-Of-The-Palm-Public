using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SettingsController : MonoBehaviour
{
    public GameObject settingsMenuObject; // Assign your in-scene Settings Menu GameObject in the Inspector
    public GameObject settingsButtonPrefab; // Assign your 3D button prefab
    public Transform playerCameraTransform; // Assign the player's camera transform
    public float buttonScreenOffsetX = 0.9f;
    public float buttonScreenOffsetY = 0.1f;
    public float buttonWorldDistance = 1.5f;
    public float menuSpawnDistance = 2.0f;

    private GameObject settingsButtonInstance;
    private Camera playerCamera;

    void Start()
    {
        if (playerCameraTransform == null)
        {
            Debug.LogError("Player Camera Transform not assigned!");
            return;
        }

        playerCamera = playerCameraTransform.GetComponent<Camera>();
        if (playerCamera == null)
        {
            Debug.LogError("Player Camera Transform does not have a Camera component!");
            return;
        }

        settingsButtonInstance = Instantiate(settingsButtonPrefab);
        if (settingsButtonInstance.TryGetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>(out var buttonInteractable))
        {
            buttonInteractable.selectEntered.AddListener(OnSettingsButtonPress);

            if (Application.isEditor)
            {
                if (settingsButtonInstance.TryGetComponent<Collider>(out var buttonCollider))
                {
                    MouseButtonOpener opener = settingsButtonInstance.AddComponent<MouseButtonOpener>();
                    opener.settingsMenuManager = this; // Still using this temporary script
                }
                else
                {
                    Debug.LogWarning("Settings button prefab has no Collider for mouse click testing.");
                }
            }
        }
        else
        {
            Debug.LogError("Settings button prefab does not have an XRSimpleInteractable component!");
        }

        UpdateSettingsButtonPosition(); // Initial positioning
    }

    void Update()
    {
        if (settingsButtonInstance != null)
        {
            UpdateSettingsButtonPosition();
            settingsButtonInstance.transform.LookAt(playerCameraTransform.position);
            settingsButtonInstance.transform.Rotate(Vector3.up, 180f);
        }
        
        if (settingsMenuObject != null && settingsMenuObject.activeSelf)
        {
            settingsMenuObject.transform.position = playerCameraTransform.position + playerCameraTransform.forward * menuSpawnDistance;
            settingsMenuObject.transform.LookAt(playerCameraTransform.position);
            settingsMenuObject.transform.Rotate(Vector3.up, 180f);
        }
    }

    void UpdateSettingsButtonPosition()
    {
        if (playerCamera != null && settingsButtonInstance != null)
        {
            Vector3 viewportPosition = new Vector3(buttonScreenOffsetX, buttonScreenOffsetY, buttonWorldDistance);
            Vector3 worldPosition = playerCamera.ViewportToWorldPoint(viewportPosition);
            settingsButtonInstance.transform.position = worldPosition;
        }
    }

    public void OnSettingsButtonPress(SelectEnterEventArgs args)
    {
        OpenSettingsMenuInFront();
    }

    public void OpenSettingsMenuInFront()
    {
        if (settingsMenuObject != null)
        {
            settingsMenuObject.SetActive(!settingsMenuObject.activeSelf);
            if (settingsMenuObject.activeSelf)
            {
                // Update position and rotation when enabling
                settingsMenuObject.transform.position = playerCameraTransform.position + playerCameraTransform.forward * menuSpawnDistance;
                settingsMenuObject.transform.LookAt(playerCameraTransform.position);
                settingsMenuObject.transform.Rotate(Vector3.up, 180f);

                // try to reset selected game object on event system; remove 
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
            }
        }
        else
        {
            Debug.LogError("Settings Menu Object not assigned in the Inspector!");
        }
    }

    // The CloseSettingsMenu function is no longer needed in this setup
    // as the OpenSettingsMenuInFront now handles toggling.
}

// Separate temporary script to handle mouse clicks on the button
public class MouseButtonOpener : MonoBehaviour
{
    [HideInInspector] public SettingsController settingsMenuManager; // Renamed to match the script

    private void OnMouseDown()
    {
        if (settingsMenuManager != null)
        {
            settingsMenuManager.OpenSettingsMenuInFront();
        }
        else
        {
            Debug.LogError("SettingsController reference not set in MouseButtonOpener.");
        }
    }
}