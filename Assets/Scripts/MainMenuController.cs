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
        LeanTween.alpha(MainScreen, 0, timeAlpha).setOnComplete(() => { MainScreen.SetActive(false); });
        GameManager.Instance.EmpezarPartida();
        SceneManager.LoadScene("GameScene");
    }

    // M�todo para bot�n 'Options'
    public void OpenOptions()
    {

        optionsMenu.SetActive(true);
        LeanTween.alpha(optionsMenu, 1f, timeAlpha);

    }

    public void BackButton()
    {

        LeanTween.alpha(optionsMenu, 0f, timeAlpha).setOnComplete(() => { optionsMenu.SetActive(false); });
    }
    public void Restart()
    {
        GameManager.Instance.ReempezarPartida();
        LeanTween.alpha(optionsMenu, 0f, timeAlpha).setOnComplete(() => { optionsMenu.SetActive(false); });
    }

    // M�todo para bot�n 'Salir'
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();

        // Para que tambi�n funcione si est�s dentro del Editor de Unity
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
