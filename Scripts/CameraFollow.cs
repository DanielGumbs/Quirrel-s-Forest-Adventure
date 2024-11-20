using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    private float minX;

    private void Start()
    {
        minX = transform.position.x;
    }

    private void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = transform.position;

            targetPosition.x = Mathf.Max(player.position.x, minX);

            targetPosition.z = -10;

            // Smoothly interpolate between the current and target positions
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        }
    }
}
