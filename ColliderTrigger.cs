using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Triggers behavior when objects on specific layers enter attached trigger collider 
/// </summary>
public class ColliderTrigger : MonoBehaviour {
    // Use cases:
    // Detect when player enters proximity of something

    // TODO:
    // Add optional functionality to check other objects physics layer

    public UnityEvent OnColliderTriggered;
    public UnityAction OnColliderTriggeredAction;

    public string[] TriggerObjectTags; // Object tags which trigger this trigger

    public bool TriggerOnlyOnce = false; // Should this trigger multiple times or once?

    bool _alreadyTriggered = false;

    private void OnTriggerEnter(Collider other) {

        if (_alreadyTriggered && TriggerOnlyOnce) return; // Do nothing if already triggered
        if (TriggerObjectTags == null) return; // Do nothing if tags are not defined

        bool isCorrectTag = false;

        // Check if other colliders tag is correct
        foreach (var tag in TriggerObjectTags) {
            if (other.gameObject.CompareTag(tag)) {
                isCorrectTag = true;
            }
        }

        if (isCorrectTag) {
            // Trigger
            // print("Trigger collider triggered");

            _alreadyTriggered = true;
            OnColliderTriggered?.Invoke();
            OnColliderTriggeredAction?.Invoke();
        }
    }
}