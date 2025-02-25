using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundsController : MonoBehaviour
{
    AudioSource AudioSource;
    AudioClip AudioClip;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        PlayMotorSound();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayMotorSound()
    {
        if (AudioSource != null)
        {
            AudioSource.Play();
        }
    }
    public void PitchSound(float pitch)
    {
        AudioSource.pitch = pitch;
        Debug.Log(AudioSource.pitch);

    }
}
