using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverText : MonoBehaviour
{
    public float hoverSpeed = 2f; // Speed of the hover effect
    public float hoverAmount = 6f; // How far it moves up and down

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.localPosition; // Stores the original position
    }

    void Update()
    {
        // Applys wave to move up and down
        float offset = Mathf.Sin(Time.time * hoverSpeed) * hoverAmount;
        transform.localPosition = startPosition + new Vector3(0, offset, 0);
    }
}
