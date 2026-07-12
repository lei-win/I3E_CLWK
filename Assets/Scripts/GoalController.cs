using UnityEngine;

public class GoalController : MonoBehaviour
{
    public int score = 0;

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the goal has the "Ball" tag
        if (other.CompareTag("Ball"))
        {
            score++;
            Debug.Log("Goal! Current Score: " + score);
            
            // Destroys the ball so you can't push it in and out to cheat the score
            Destroy(other.gameObject); 
        }
    }
}