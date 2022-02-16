using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // Plays a random music track

    public string[] musicTracks;

    void Start()
    {
        int randomIndex = Random.Range(0, musicTracks.Length);

        AudioManager.instance.Play(musicTracks[randomIndex]);
    }

}
