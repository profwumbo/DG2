using UnityEngine;

public class cam : MonoBehaviour
{
    public Transform player;           // Assign your player in Inspector
    public float smoothSpeed = 0.1f;   // Smooth movement speed

    private float initialZ;

    void Start()
    {
        if (player == null) return;

        // Keep the original camera Z (depth)
        initialZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Target position based on player's X and Y
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, initialZ);

        // Smoothly move camera
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }
}
