using UnityEngine;
using UnityEngine.InputSystem; // Added this

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    public float kickForce = 10f;
    public InputActionReference interactAction; // Slot in the Inspector
    
    private bool isPlayerNear = false;
    private Rigidbody rb;
    private Transform playerTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (interactAction != null) interactAction.action.Enable();
    }

    void Update()
    {
        if (isPlayerNear && interactAction.action.WasPressedThisFrame())
        {
            Vector3 pushDirection = (transform.position - playerTransform.position).normalized;
            pushDirection.y = 0.5f; 
            rb.AddForce(pushDirection * kickForce, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            playerTransform = other.transform; 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            playerTransform = null;
        }
    }
}