using UnityEngine;

public class OscillatingDot : MonoBehaviour
{
    public Vector3 initialPosition = new Vector3(0,0,0);  // Initial position of the point
    public float radius = 0.01f; // Radius of the oscillation
    public float speed = 0.04f;  // Speed of the oscillation
    private float angle = 0.0f; // Current angle

    private void Update()
    {
        // Update the angle based on the speed and time
        angle += speed * Time.deltaTime;

        // Ensure the angle stays within 0 to 2*PI range
        if (angle > Mathf.PI * 2) angle -= Mathf.PI * 2;

        // Calculate the new position using trigonometric functions
        float x = transform.position.x + radius * Mathf.Cos(angle);
        float y = transform.position.y + radius * Mathf.Sin(angle);

        // Set the new position of the GameObject
        transform.Rotate(0.001f * x, 0.001f * y, 0);
    }
}