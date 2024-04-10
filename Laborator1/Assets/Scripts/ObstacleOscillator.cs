using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleOscillator : MonoBehaviour
{
    public float amplitude = 5f;
    public float frequency = 1f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float newY = startPosition.y + amplitude * Mathf.Sin(Time.time * frequency);
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
