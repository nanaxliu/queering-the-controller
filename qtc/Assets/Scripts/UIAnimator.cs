using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimator : MonoBehaviour
{
    public Image imageComponent; // The UI Image to animate
    public Sprite[] sprites; // Array of sprites to cycle through
    public float frameRate = 0.2f; // Time per frame (seconds)

    private int currentFrame = 0;

    void Start()
    {
        if (sprites.Length > 0)
        {
            StartCoroutine(AnimateSprite());
        }
    }

    IEnumerator AnimateSprite()
    {
        while (true)
        {
            imageComponent.sprite = sprites[currentFrame]; // Switch sprite
            currentFrame = (currentFrame + 1) % sprites.Length; // Loop animation
            yield return new WaitForSeconds(frameRate); // Wait before switching again
        }
    }
}
