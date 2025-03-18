using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMover : MonoBehaviour
{
    public enum CloudSize { Big, Medium, Small }
    public CloudSize cloudSize;

    public int pixelsPerUnit = 16;
    public float frameRate = 1f; // Delay between each step
    private float nextMoveTime;

    private int pixelStep; // Movement per frame in pixel units

    void Start()
    {
        // Dif speeds for different cloud sizes 
        switch (cloudSize)
        {
            case CloudSize.Big:
                pixelStep = 10; // Slowest 
                break;
            case CloudSize.Medium:
                pixelStep = 12; // Medium 
                break;
            case CloudSize.Small:
                pixelStep = 14; // Fastest 
                break;
        }

        nextMoveTime = Time.time;
    }

    void Update()
    {
        // Moves clouds in pixel steps at fixed intervals
        if (Time.time >= nextMoveTime)
        {
            // Moves exactly 1 pixel per step, converted to Unity units
            transform.position += new Vector3(-pixelStep / (float)pixelsPerUnit, 0, 0);
            nextMoveTime = Time.time + frameRate; // Waits before moving again
        }
    }
}
