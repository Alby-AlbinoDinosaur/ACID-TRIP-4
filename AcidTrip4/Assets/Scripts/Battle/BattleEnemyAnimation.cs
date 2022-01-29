using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnemyAnimation : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public enum AnimationTypes
    {
        frameByFrame,
    }
    public AnimationTypes animationType;

    public bool paused = false;

    // FBF parameters.
    public Sprite[] FBFSprites; // Frame by frame: iterates between these sprites every interval.
    int FBFIndex = 0; // The index of the current frame.
    public float FBFInterval; // 0.0416 for 24 fps.
    float FBFCooldown;

    // Damage animation.
    public Sprite damageSprite; // The sprite to show when the enemy is damaged.
    public float damageIntensity; // Default intensity.
    float currentDamageIntensity; // If this is ever above 0, activate the damage animation.
    public float damageSpeed;
    public float damageFalloff;
    float deltaX;
    Vector3 originalDamagePos; // Local coords.

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Initialize damage parameters.
        originalDamagePos = transform.localPosition;

        if (animationType == AnimationTypes.frameByFrame) // Initialize FBF parameters.
        {
            FBFCooldown = FBFInterval;
            spriteRenderer.sprite = FBFSprites[FBFIndex];
        }
    }

    void Update()
    {
        if (!paused)
        {
            switch (animationType)
            {
                case AnimationTypes.frameByFrame:
                    FBFUpdate();
                    break;
            }
        }

        if (currentDamageIntensity > 0)
        {
            DamageUpdate();
        }
    }

    void FBFUpdate()
    {
        // What happens every frame if Frame by Frame animation is selected:
        // Wait for the cooldown to end, then switch sprite to the next one in FBFSprites.

        FBFCooldown -= Time.deltaTime;

        if (FBFCooldown <= 0)
        {
            // Change the sprite to the next one, as dictated by FBFIndex.
            int newIndex = Mathf.RoundToInt(Mathf.Repeat(FBFIndex + 1, FBFSprites.Length - 1));
            spriteRenderer.sprite = FBFSprites[newIndex];
            FBFIndex = newIndex;

            // Reset cooldown.
            FBFCooldown = FBFInterval;
        }
    }

    public void DamageAnimation()
    {
        // When this is called, the sprite switches to damageSprite and the enemy jiggles back and forth a little.
        // This pauses the current animation until the jiggling stops.

        if (currentDamageIntensity <= 0)
        {
            originalDamagePos = transform.localPosition; // Set the anchor for the damage shake.
            currentDamageIntensity = damageIntensity;
            deltaX = damageSpeed;
            paused = true;
            spriteRenderer.sprite = damageSprite;
        }
        else // If currentDamageIntensity > 0, there's already a damage shake in progress, so just replace those values with these ones instead of starting a new shake.
        {
            currentDamageIntensity = damageIntensity;
            deltaX = damageSpeed;
        }
    }
    void DamageUpdate()
    {
        // What happens every frame if the enemy is currently being damaged:
        // X position oscillates depending on damage parameters. When the movement gets small enough, stop being damaged.

        // If x is outside of the damageIntensity bounds, invert deltaX and teleport the sprite within the bounds.
        if (transform.localPosition.x > (originalDamagePos.x + currentDamageIntensity))
        {
            deltaX = -deltaX;
            transform.localPosition = new Vector3(originalDamagePos.x + currentDamageIntensity, transform.localPosition.y, transform.localPosition.z);
        }
        if (transform.localPosition.x < (originalDamagePos.x - currentDamageIntensity))
        {
            deltaX = -deltaX;
            transform.localPosition = new Vector3(originalDamagePos.x - currentDamageIntensity, transform.localPosition.y, transform.localPosition.z);
        }

        transform.localPosition += new Vector3(deltaX * Time.deltaTime, 0, 0);

        // Shrink the damageIntensity over time. When it hits 0, reset everything. Then, DamageUpdate() should stop activating.
        currentDamageIntensity -= damageFalloff * Time.deltaTime;
        if (currentDamageIntensity <= 0)
        {
            paused = false;
            transform.localPosition = originalDamagePos;
        }
    }
}
