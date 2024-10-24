using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possum : MonoBehaviour
{
    public List<Transform> points;
    public float moveSpeed = 5f; // Adjust this to control the speed of movement
    private int nextPointIndex = 0;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (points == null || points.Count == 0)
        {
            points = new List<Transform>();
            foreach (Transform child in transform.Find("Waypoints"))
            {
                points.Add(child);
            }
        }

        MoveToNextPoint();
    }

    private void MoveToNextPoint()
    {
        if (points.Count == 0)
            return;

        Vector3 direction = (points[nextPointIndex].position - transform.position).normalized;

        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, points[nextPointIndex].position) < 0.1f)
        {
            nextPointIndex = (nextPointIndex + 1) % points.Count;

            if (direction.x < 0)
                spriteRenderer.flipX = true;
            else if (direction.x > 0)
                spriteRenderer.flipX = false;
        }
    }

    private void Update()
    {
        // Move towards the next point each frame
        MoveToNextPoint();
    }
}
