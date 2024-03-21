using MyBox;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Moves the parent object around with smooth easing.
/// Can be ordered to start and stop. 
/// Invokes unity events when movement starts or stops
/// </summary>
public class SimpleMover : MonoBehaviour {

    [Tooltip("The maximum speed the object can move at.")]
    public float maxMoveSpeed = 5f;

    [Tooltip("The rate at which the object speeds up or slows down.")]
    public float acceleration = 2f;

    [Tooltip("The direction the object will move in.")]
    public MoveDirection moveDirection;

    [Tooltip("Should the object move automatically on start?")]
    public bool MoveOnStart = false;

    [Tooltip("Event triggered when the object starts moving.")]
    public UnityEvent onStartMovement;

    [Tooltip("Event triggered when the object stops moving.")]
    public UnityEvent onStopMovement;

    private float currentSpeed = 0f;
    private bool isMoving = false;

    public enum MoveDirection { Forward, Left, Back, Down }

    private void Start() {
        if (MoveOnStart)
        {
            StartMovement();
        }
    }

    void Update() {
        HandleMovement();
    }

    public void StartMovement() {
        isMoving = true;
        onStartMovement.Invoke();
    }

    public void StopMovement() {
        isMoving = false;
        onStopMovement.Invoke();
    }

    private void HandleMovement() {
        if (isMoving) {
            currentSpeed = Mathf.Min(maxMoveSpeed, currentSpeed + acceleration * Time.deltaTime);
        } else {
            currentSpeed = Mathf.Max(0, currentSpeed - acceleration * Time.deltaTime);
        }

        if (currentSpeed > 0) {
            Vector3 direction = GetDirection();
            transform.Translate(direction * currentSpeed * Time.deltaTime);
        }
    }

    private Vector3 GetDirection() {
        switch (moveDirection) {
            case MoveDirection.Left:
                return Vector3.left;
            case MoveDirection.Back:
                return Vector3.back;
            case MoveDirection.Down:
                return Vector3.down;
            default:
                return Vector3.forward;
        }
    }
}

