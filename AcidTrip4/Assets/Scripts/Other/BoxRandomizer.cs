using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxRandomizer : MonoBehaviour
{
    // Randomizes the appearance of a box to one of several different sprites.
    // Activates on start. Should also activate whenever a box appears on screen; use RandomizeBoxSprite() from another script for that.

    SpriteRenderer spriteRenderer;
    public Sprite[] boxSprites;
   
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        RandomizeBoxSprite();
    }

    public void RandomizeBoxSprite()
    {
        int spriteIndex = Random.Range(0, boxSprites.Length);
        spriteRenderer.sprite = boxSprites[spriteIndex];
    }
}
