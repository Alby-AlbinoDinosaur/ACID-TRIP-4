using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // Plays a random music track, and loops it.
    // Very simple, probably ought to be replaced with something more complex.

    AudioSource audioSource;

    public AudioClip[] music;
    AudioClip chosenTrack;

    float timeUntilLoop;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        int chosenTrackIndex = Random.Range(0, music.Length);
        chosenTrack = music[chosenTrackIndex];

        audioSource.PlayOneShot(chosenTrack);
        timeUntilLoop = chosenTrack.length;
    }

    void Update()
    {
        timeUntilLoop -= Time.deltaTime;
        if (timeUntilLoop <= 0)
        {
            audioSource.PlayOneShot(chosenTrack);
            timeUntilLoop = chosenTrack.length;
        }
    }
}
