using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundEvents : MonoBehaviour
{
    public void Play(string soundName)
    {
        AudioManager.instance.PlayOneShot(soundName);
    }

    private void Start()
    {
        
    }
}
