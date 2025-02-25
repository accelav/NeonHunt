using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [Header("Audio Mixer")]
    public AudioMixer mainMixer;

    [Header("Sliders")]
    public Slider masterSlider;
    public Slider sfxSlider;
    public Slider ambienceSlider;
    public Slider musicSlider;
    // Ajustamos el volumen al iniciar, tomando posibles valores guardados
    private void Start()
    {
        // Recuperar valores guardados (si usas PlayerPrefs u otro sistema)
        // Ejemplo (por defecto 1.0f si no existe la key):
        float masterVol = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
        float amVol = PlayerPrefs.GetFloat("AmbienceVolume", 1.0f);
        float muVol = PlayerPrefs.GetFloat("MusicVolume", 1.0f);

        // Asignar esos valores a los sliders
        masterSlider.value = masterVol;
        sfxSlider.value = sfxVol;
        ambienceSlider.value = amVol;
        musicSlider.value = muVol;

        // Reflejar en el AudioMixer
        SetMasterVolume(masterVol);
        SetSFXVolume(sfxVol);
        SetAmbienceVolume(amVol);
        SetMusicVolume(muVol);
    }

    // Estas funciones se pueden llamar desde OnValueChanged de los Sliders en el Inspector

    public void SetMasterVolume(float volume)
    {
        // Convertir 0-1 a dB. A 1 => 0dB, a 0.0001 => ~-80dB
        mainMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20f);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        mainMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20f);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetAmbienceVolume(float volume)
    {
        mainMixer.SetFloat("AmbienceVolume", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20f);
        PlayerPrefs.SetFloat("AmbienceVolume", volume);
    }
    public void SetMusicVolume(float volume)
    {
        mainMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20f);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
}

