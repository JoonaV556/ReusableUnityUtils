using UnityEngine;

/// <summary>
/// Adds a up-and-down floating effect to world objects. Useful for ui windows etc.
/// </summary>
public class FloatUpAndDown : MonoBehaviour
{
    public float FloatSpeed = 1f; // How fast the floating happens

    public float FloatMovementMagnitude = 0.5f; // How much the object moves when floating

    float LerpAlpha = 0f;

    // Transform components Y-peak positions
    float LowYPositon;
    float HighYPositon;

    int direction = 1; // Direction of lerping (1 = positive, -1 = negative)

    private void Start() {
        LowYPositon = transform.position.y;
        HighYPositon = transform.position.y;
        // Calculate peak positions
        LowYPositon -= 1 * FloatMovementMagnitude;
        HighYPositon += 1 * FloatMovementMagnitude;
    }

    private void Update() {
        Vector3 currentPosition = transform.position;

        // Flip lerp directions
        if (LerpAlpha <= 0) {
            direction = 1;
        } 
        if (LerpAlpha >= 1) {
            direction = -1;
        } 

        // Increse / decrease lerp alpha
        LerpAlpha += direction * (Time.deltaTime / FloatSpeed);

        // Do actual lerpig
        float ModifiedYposition;
        ModifiedYposition = Mathf.SmoothStep(LowYPositon, HighYPositon, LerpAlpha);

        // Apply new position
        transform.position = new Vector3(currentPosition.x, ModifiedYposition, currentPosition.z);
    }
}