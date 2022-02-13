using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnemyAnimation : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public enum AnimationTypes
    {
        frameByFrame, squash,
    }
    public AnimationTypes animationType;

    // ==NOTES==
    // Frame by frame: iterates between a set of sprites every interval.
    //      If unpaused, FBF will use a preset order of specific sprites, so other sprites cannot be used.
    // Squash: Squishes and stretches the sprite with a sine wave.
    //      If unpaused, squash will use this object's transform.localScale, so if you want to resize the sprite, use the parent object instead.
    //      Squash works best when the sprite's pivot is on the bottom.

    public bool paused = false;

    float timer = 0; // Number of seconds that this sprite has gone without being paused.

    // FBF parameters.
    public Sprite[] FBFSprites; // 
    int FBFIndex = 0; // The index of the current frame.
    public float FBFInterval; // 0.0416 for 24 fps.
    float FBFCooldown;

    // Squash parameters.
    public float squashSpeed; // Speed at which to squash sprite.
    public float squashAmplitude; // Magnitude of squashing.
    public bool squashHardBounce; // This applies an absolute value to the squash, resulting in a slightly different animation.
    public float squashXMultiplier; // Multiply the x-scaling by this amount. Usually <1, to limit the sideways stretching because that looks strange.
    Vector3 squashOriginalScale;

    // Damage animation.
    Sprite previousSprite;
    public Sprite damageSprite; // The sprite to show when the enemy is damaged.
    public float damageIntensity; // Default intensity.
    float currentDamageIntensity; // If this is ever above 0, activate the damage animation.
    public float damageIntensityFalloff;
    public float damageSpeed;
    public float damageSpeedFalloff;

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

        if (animationType == AnimationTypes.squash) // Initialize squash parameters.
        {
            squashOriginalScale = transform.localScale;
        }
    }

    void Update()
    {
        if (!paused)
        {
            timer += Time.deltaTime; // If not paused, increment the timer, which can be used for any animation.

            switch (animationType)
            {
                case AnimationTypes.frameByFrame:
                    FBFUpdate();
                    break;
                case AnimationTypes.squash:
                    SquashUpdate();
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

    void SquashUpdate()
    {
        // What happens every frame if Squash animation is selected:
        // Increase the y scaling on a sin wave and the x scaling on a cos wave (multiplied by squashXMultiplier).

        float deltaY = (Mathf.Sin(timer * squashSpeed) * squashAmplitude);
        if (squashHardBounce) { deltaY = Mathf.Abs(deltaY); }

        float hardBounceXFactor = 1;
        if (squashHardBounce) { hardBounceXFactor = 2; } // If squashHardBounce is on, the X should go twice as fast to keep up with the absolute Y squash. Otherwise it doesn't look right.
        float deltaX = (Mathf.Cos(timer * squashSpeed * hardBounceXFactor) * squashAmplitude * squashXMultiplier); // May be 0 if squashXMultiplier is 0.

        transform.localScale = new Vector3(squashOriginalScale.x + deltaX, squashOriginalScale.y + deltaY, squashOriginalScale.z);
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

            previousSprite = spriteRenderer.sprite; // Save the old sprite so you can return to that sprite after the damage animation.
            spriteRenderer.sprite = damageSprite;

            if (animationType == AnimationTypes.squash)
            {
                transform.localScale = squashOriginalScale; // Reset the local scale, since Squash changes it.
                timer = 0; // Reset the timer so the squash doesn't restart in a weird spot.
            }
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

        // Shrink the deltaX over time.
        float signDeltaX = Mathf.Sign(deltaX);
        float absDeltaX = Mathf.Abs(deltaX);
        float shrinkAmount = damageSpeedFalloff * Time.deltaTime;
        deltaX = signDeltaX * (absDeltaX - shrinkAmount);

        // Shrink the damageIntensity over time. When it hits 0, reset everything. Then, DamageUpdate() should stop activating.
        currentDamageIntensity -= damageIntensityFalloff * Time.deltaTime;
        if (currentDamageIntensity <= 0)
        {
            spriteRenderer.sprite = previousSprite; // Go back to the original sprite.
            paused = false;
            transform.localPosition = originalDamagePos;
        }
    }
}
