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

    public void stopAllTracks()
    {  
        for(int i = 0; i < musicTracks.Length; i++)
        {
            AudioManager.instance.Stop(musicTracks[i]);
        }

    }

}
