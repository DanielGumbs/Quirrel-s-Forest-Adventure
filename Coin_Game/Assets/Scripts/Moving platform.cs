using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private List<Transform> points;
    private int nextPointIndex = 0;

    private void Start()
    {
        if (points == null || points.Count == 0)
        {
            points = new List<Transform>();
            foreach (Transform child in transform.Find("Waypoints"))
            {
                points.Add(child);
            }
        }
    }

    private void Update()
    {
        MoveToNextPoint();
    }

    private void MoveToNextPoint()
    {
        if (points.Count == 0)
            return;

        // Calculate direction towards the next point
        Vector3 direction = (points[nextPointIndex].position - transform.position).normalized;

        // Move towards the next point
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

        // If close enough to the next point, switch to the next point
        if (Vector3.Distance(transform.position, points[nextPointIndex].position) < 0.1f)
        {
            nextPointIndex = (nextPointIndex + 1) % points.Count;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            // Calculate the velocity difference between the platform and the player
            Vector2 velocityDifference = GetComponent<Rigidbody2D>().velocity - playerRigidbody.velocity;

            // Apply the velocity difference to the player
            playerRigidbody.velocity += velocityDifference;

            // Adjust the player's position relative to the platform
            Vector3 positionDifference = transform.position - collision.transform.position;
            collision.transform.position += positionDifference;
        }
    }
}
