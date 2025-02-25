using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsBehaviour : MonoBehaviour
{
    public static SoundsBehaviour instance;
    AudioSource audioSource;
    public AudioClip[] clip;
    /*
    Clip 0 = Boton1
    Clip 1 = Boton2
    Clip 2 = Boton3
    Clip 3 = Deteccion
    Clip 4 = Disparo
    Clip 5 = Explosion1
    Clip 6 = Explosion2
    Clip 7 = Recarga

    */
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayButtonSound()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip[0]);
        }
    }
    public void PlayButtonSoundTwo()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip[1]);
        }
    }
    public void PlayButtonSoundTres()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip[2]);
        }
    }
    public void PlayDetectSound()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip[3]);
        }
    }
    public void PlayShootSound()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip[4]);
        }
    }
    public void PlayExplosion()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip[5]);
        }
    }
    public void PlayExplosionTwo()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip[6]);
        }
    }
    public void PlayRecargaSound()
    {
        if(audioSource != null)
        {
            audioSource.PlayOneShot(clip[7]);
        }
    }

}
