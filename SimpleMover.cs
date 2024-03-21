using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Moves the parent object around. 
/// Can be ordered to start and stop. 
/// Invokes unity events when movement starts or stops
/// </summary>
public class SimpleMover : MonoBehaviour {

    public enum MoveDirection { Forward, Left, Back, Down } // Enum for move direction 
    public MoveDirection moveDirection = MoveDirection.Forward; // Variable to hold the direction of movement

    [Space(10)]
    public float moveSpeed = 5f;

    // Space attribute
    [Space(10)]
    public UnityEvent onStartMovement;
    public UnityEvent onStopMovement;

    private bool isMoving = false;

    void Update() {
        if (isMoving) {
            Vector3 direction;

            // Determine the direction of movement based on the value of moveDirection
            switch (moveDirection) {
                case MoveDirection.Left:
                    direction = Vector3.left;
                    break;
                case MoveDirection.Back:
                    direction = Vector3.back;
                    break;
                case MoveDirection.Down:
                    direction = Vector3.down;
                    break;
                default: // Default to forward if no valid direction is provided
                    direction = Vector3.forward;
                    break;
            }

            // Move the object in the determined direction
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }

    public void StartMovement() {
        isMoving = true;
        onStartMovement.Invoke();
    }

    public void StopMovement() {
        isMoving = false;
        onStopMovement.Invoke();
    }
}

