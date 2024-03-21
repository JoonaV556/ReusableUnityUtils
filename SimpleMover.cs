using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Moves the parent object around. 
/// Can be ordered to start and stop. 
/// Invokes unity events when movement starts or stops
/// </summary>
public class SimpleMover : MonoBehaviour {

    public float moveSpeed = 5f; // Default movement speed
    private bool isMoving = false; // Flag to track movement state

    public UnityEvent onStartMovement; // UnityEvent triggered when movement starts
    public UnityEvent onStopMovement; // UnityEvent triggered when movement stops

    // Update is called once per frame
    void Update() {
        // Check if the object should be moving
        if (isMoving) {
            // Move the object forwards based on the defined speed
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }

    // Public function to start the movement
    public void StartMovement() {
        isMoving = true;
        onStartMovement.Invoke(); // Trigger the UnityEvent when movement starts
    }

    // Public function to stop the movement
    public void StopMovement() {
        isMoving = false;
        onStopMovement.Invoke(); // Trigger the UnityEvent when movement stops
    }
}