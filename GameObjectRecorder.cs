using MyBox;
using UnityEditor.Animations;
using UnityEngine;

public class GameObjectRecorderUtility : MonoBehaviour {
    // Please note: MyBox utils package is required for the [ButtonMethod] attributes to work. If you don't wanna use it, just remove the attributes.
    
    // Utility component for recording gameobjects.
    // Uses the Unity recorder package and GameObjectRecorder class to record gameobject movement to reusable animation clips.

    // Records only transform components by default 
    // Optionally records a gameobject and all of its children too

    // Enum to represent the recording state
    public enum RecordingState {
        Recording,
        NotRecording
    }

    [Tooltip("The AnimationClip where the recording will be saved.")]
    public AnimationClip DestinationClip; // The destination clip for the recording

    [Tooltip("The GameObject that will be recorded.")]
    public GameObject RecordTargetObj; // The target object to record

    [Tooltip("Should the recording include the children of the target GameObject.")]
    public bool RecordChildren = false; // Flag to determine if children should be recorded

    [Tooltip("The current state of the recording.")]
    public RecordingState recordingState = RecordingState.NotRecording; // The current recording state


    GameObjectRecorder recorder; // The GameObjectRecorder instance

    string cannotRecordMessage = " Cannot record."; // Message to display when recording cannot be started

    bool shouldRecord = false; // Flag to determine if recording should be started

    [ButtonMethod]
    public void StartRecording() {
        // Check if already recording
        if (recorder != null && recorder.isRecording) {
            Debug.LogWarning("Already recording." + cannotRecordMessage);
            return;
        }

        // Check if target object is assigned
        if (RecordTargetObj == null) {
            Debug.LogWarning("No target object assigned." + cannotRecordMessage);
            return;
        }

        // Check if recording is already in progress
        if (shouldRecord == true) {
            Debug.LogWarning("Cannot start recording. Recording already in progress.");
            return;
        }

        // Create new recorder   
        recorder = new GameObjectRecorder(RecordTargetObj);
        // Bind target components for the recording
        recorder.BindComponentsOfType<Transform>(RecordTargetObj, RecordChildren);
        // Start recording
        shouldRecord = true;
    }

    [ButtonMethod]
    public void StopAndSave() {
        // Check if recording is not in progress
        if (shouldRecord == false) {
            Debug.Log("Cannot stop and save. Recorder is not recording.");
            return;
        }

        // Check if destination clip is assigned
        if (DestinationClip == null) {
            Debug.LogWarning("Cannot save recorded clip. No destination clip to save to");
            return;
        }

        // Stop recording
        shouldRecord = false;
        // Save the recording to anim clip
        recorder.SaveToClip(DestinationClip, 60f);
        Debug.Log("Recorded clip of length: " + recorder.currentTime);
        // Clear recording
        recorder.ResetRecording();
        // Destroy recorder
        recorder = null;
    }

    // Do recording
    private void LateUpdate() {
        // Check if recording should be done
        if (recorder != null && shouldRecord == true) {
            recordingState = RecordingState.Recording;
            recorder.TakeSnapshot(Time.deltaTime);
        } else {
            recordingState = RecordingState.NotRecording;
        }
    }
}
