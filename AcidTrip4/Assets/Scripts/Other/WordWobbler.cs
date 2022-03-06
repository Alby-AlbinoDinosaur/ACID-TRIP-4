using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordWobbler : MonoBehaviour
{
    // Goes through a list of letters, displaying them to the right.
    // Makes them move up and down in a sine wave.
    // This should go in a canvas object.

    public List<Sprite> letterSprites;
    public List<float> xOffsets; // Both of these must be the same length as letterSprites.
    public List<float> yOffsets;

    List<GameObject> letters = new List<GameObject>(); // After creation, letters are stored in here.

    // Wobble parameters.
    public float wobbleSpeed;
    public float wobbleAmplitude;
    public float wobbleWidth;

    float timer = 0;
    bool toggleWobble = true; // If false, disable wobble. This is used by ToggleWobble(), which is used by the button that stops background wobbling.

    // Visual effects.
    public float letterScale = 1; // Scaling multiplier.

    void Start()
    {
        if (xOffsets.Count != letterSprites.Count)
        { Debug.LogError($"{gameObject.name}: WordWobbler doesn't have the same number of sprites as x-offsets. ({letterSprites.Count} vs. {xOffsets.Count})"); }
        if (yOffsets.Count != letterSprites.Count)
        { Debug.LogError($"{gameObject.name}: WordWobbler doesn't have the same number of sprites as y-offsets. ({letterSprites.Count} vs. {yOffsets.Count})"); }

        // For each sprite in letterSprites, create a new object and parent it to this one.

        int i = 0;
        foreach (Sprite newLetterSprite in letterSprites)
        {
            GameObject newLetter = new GameObject($"letter{i}");
            newLetter.transform.parent = transform;
            letters.Add(newLetter);

            // Set the letter's sprite.
            Image newLetterImage = newLetter.AddComponent<Image>();
            newLetterImage.sprite = letterSprites[i];
            newLetterImage.SetNativeSize();
            newLetter.transform.localScale = letterScale * newLetter.transform.localScale; // Scale the letter. This does not affect its offsets.

            i++;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Go through letters from left to right order.
        // Offset each letter from the previous letter by the xOffsets and yOffsets.
        int i = 0;
        Vector3 oldLetterPosition = transform.position;
        foreach (GameObject letter in letters)
        {
            // Move the letter to the old letter position, plus offsets.
            Vector3 newLetterPosition = oldLetterPosition + new Vector3(xOffsets[i], yOffsets[i], 0);
            letter.transform.position = newLetterPosition;
            oldLetterPosition = newLetterPosition;

            i++;
        }

        if (toggleWobble)
        {
            // Move the letters up and down in a sine wave.
            int letterNum = 0;
            foreach (GameObject letter in letters)
            {
                // Add up the total x offset of the letter and add it to the timer for a more consistent wave.
                float totalXOffset = 0;
                for (int j = 0; j <= letterNum; j++)
                {
                    totalXOffset += xOffsets[j];
                }
                //Debug.Log($"Letter {letterNum} has totalXOffset {totalXOffset}.");

                float deltaY = Mathf.Sin((timer * wobbleSpeed) + (totalXOffset * wobbleWidth)) * wobbleAmplitude;
                letter.transform.position += new Vector3(0, deltaY, 0);

                letterNum++;
            }
        }
    }

    public void ToggleWobble()
    {
        // Toggle wobbling on or off.
        // Used by a button in the settings menu.
        toggleWobble = !toggleWobble;
    }
}
