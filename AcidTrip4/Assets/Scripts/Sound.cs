using UnityEngine;

[System.Serializable]
public class Sound
{
    //Brackeys: Introduction to AUDIO in Unity
    //https://youtu.be/6OT43pvUyfY

    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;


}
