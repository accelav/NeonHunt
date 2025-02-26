using UnityEngine;
using UnityEngine.SceneManagement; // Para cargar escenas
#if UNITY_EDITOR
using UnityEditor;                // Para salir desde el editor
#endif
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public GameObject MainScreen;
    public GameObject optionsMenu;
    public float timeAlpha;

    private void Start()
    {
        optionsMenu.SetActive(false);
        LeanTween.alpha(optionsMenu, 0f, 0f);

    }

    private void Update()
    {


    }
    public void PlayGame()
    {
        SoundsBehaviour.instance.PlayButtonSound();
        //GameManager.Instance.estaContando = true;
        MainScreen.SetActive(false);
        GameManager.Instance.EmpezarPartida();
        //SceneManager.LoadScene("GameScene");
    }

    // Método para botón 'Options'
    public void OpenOptions()
    {
        SoundsBehaviour.instance.PlayButtonSound();
        optionsMenu.SetActive(true);
        LeanTween.alpha(optionsMenu, 1f, timeAlpha);

    }

    public void BackButton()
    {
        SoundsBehaviour.instance.PlayButtonSoundTwo();
        LeanTween.alpha(optionsMenu, 0f, timeAlpha).setOnComplete(() => { optionsMenu.SetActive(false); });
    }
    public void Restart()
    {
        SoundsBehaviour.instance.PlayButtonSoundTres();
        GameManager.Instance.ReempezarPartida();
        LeanTween.alpha(optionsMenu, 0f, timeAlpha).setOnComplete(() => { optionsMenu.SetActive(false); });
    }

    // Método para botón 'Salir'
    public void QuitGame()
    {
        SoundsBehaviour.instance.PlayButtonSoundTres();
        Debug.Log("Saliendo del juego...");
        Application.Quit();

        // Para que también funcione si estás dentro del Editor de Unity
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
