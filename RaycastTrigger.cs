using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Triggers events when given transform faces towards specific objects
/// </summary>
public class RaycastTrigger : MonoBehaviour
{
    // Triggers desired behavior when given transform faces towards specific objects.
    // Behavior can be triggered with UnityEvents in the inspector or UnityActions in code
    //
    // Possible use cases:
    // - Triggering custom behavior when player looks at a specific object


    // Triggered events
    public UnityEvent OnRayHit;
    public UnityAction OnRayHitAction;

    // Transform to use for the raycast origin & direction
    public Transform RayOriginTransform;

    // Use this to define which layers trigger the behavior
    public LayerMask RayLayerMask; // Ray triggers only when it hits objects with these layers

    public float RayMaxDistance = Mathf.Infinity; // Maximum distance the ray can travel

    public bool StopRaycastingAfterHit = true; // Should raycasting be stopped after hitting something?
    public bool DestroyAfterHit = false; // Should this component be destroyed after the ray hits something and events have been triggered?

    bool _enabled = true;

    /// <summary>
    /// Enables the raycasting in case its stopped
    /// </summary>
    public void Enable() {
        _enabled = true;
    }

    private void Start() {
        if (RayOriginTransform == null) {
            RayOriginTransform = this.transform; // Get ref to ray origin if its not assigned in inspector
        }
    }

    private void Update() {

        if (_enabled == false) return; // Do nothing if raycasting is stopped

        DoRaycasting();
    }

    private void DoRaycasting() {
        Vector3 rayOriginPosition = transform.position;

        bool _rayHitAnything = Physics.Raycast(rayOriginPosition, RayOriginTransform.forward, Mathf.Infinity, RayLayerMask);

        if (_rayHitAnything) {
            // print("Trigger ray hit something");

            // Invoke events
            OnRayHit?.Invoke();
            OnRayHitAction?.Invoke();

            // Stop raycasting 
            if (StopRaycastingAfterHit) _enabled = false;

            // Destroy
            if (DestroyAfterHit) Destroy(this);
        }
    }

}