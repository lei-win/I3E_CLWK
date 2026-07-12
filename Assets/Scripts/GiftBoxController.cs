using UnityEngine;
using UnityEngine.InputSystem; // Added this

public class GiftBoxController : MonoBehaviour
{
    [Header("Settings")]
    public int requiredPresses = 3;
    public GameObject ballPrefab; 
    
    [Header("Input Setup")]
    public InputActionReference interactAction; // This creates a slot in the Inspector
    
    private int currentPresses = 0;
    private bool isPlayerNear = false;

    void Start()
    {
        // We have to turn the action on so it listens for the button press
        if (interactAction != null) interactAction.action.Enable();
    }

    void Update()
    {
        // Check if player is near AND the action was triggered this frame
        if (isPlayerNear && interactAction.action.WasPressedThisFrame())
        {
            currentPresses++;
            Debug.Log("Box pressed: " + currentPresses);

            if (currentPresses >= requiredPresses)
            {
                Instantiate(ballPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) isPlayerNear = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) isPlayerNear = false;
    }
}