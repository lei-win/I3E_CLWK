using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    public float kickForce = 10f;
    public InputActionReference interactAction; 
    
    private bool isPlayerNear = false;
    private Rigidbody rb;
    private Transform playerTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // FREEZE the ball so bumping into it does nothing
        rb.isKinematic = true; 
        
        if (interactAction != null) interactAction.action.Enable();
    }

    void Update()
    {
        if (isPlayerNear && interactAction.action.WasPressedThisFrame())
        {
            // UNFREEZE the ball right before we kick it
            rb.isKinematic = false; 

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