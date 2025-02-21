using UnityEngine;
using UnityEngine.SceneManagement; // Para cargar escenas
#if UNITY_EDITOR
using UnityEditor;                // Para salir desde el editor
#endif
using TMPro;

public class ScreensController : MonoBehaviour
{
    public GameObject MainScreen;
    public GameObject optionsMenu;
    public GameObject optionsMenuInGame;
    public float timeAlpha;
    [SerializeField]
    TextMeshProUGUI warningText;
    [SerializeField]
    TextMeshProUGUI timerText;
    [SerializeField]
    TextMeshProUGUI pointsText;

    private void Start()
    {
        optionsMenu.SetActive(false);
        optionsMenuInGame.SetActive(false);
        LeanTween.alpha(optionsMenu, 0f, 0f);

    }

    private void Update()
    {
        if (GameManager.Instance.partidaEmpezada)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OpenOptionsInGame();
            }
        }
        if (GameManager.Instance.partidaEmpezada == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                BackButton();
            }
        }
        if (GameManager.Instance.estaDetectando)
        {
            warningText.text = "Detectado";
        }
        if (GameManager.Instance.estaDetectando == false)
        {
            warningText.text = "Sigilo";
        }

    }
    public void PlayGame()
    {
        LeanTween.alpha(MainScreen, 0, timeAlpha).setOnComplete(() => { MainScreen.SetActive(false); });
        GameManager.Instance.EmpezarPartida();
    }

    // Método para botón 'Options'
    public void OpenOptions()
    {
        GameManager.Instance.PausarPartida();
        optionsMenu.SetActive(true);
        LeanTween.alpha(optionsMenu, 1f, timeAlpha);

    }
    public void OpenOptionsInGame()
    {
        GameManager.Instance.PausarPartida();
        optionsMenuInGame.SetActive(true);
        LeanTween.alpha(optionsMenuInGame, 1f, timeAlpha);

    }

    public void BackButton()
    {
        GameManager.Instance.PausarPartida();
        LeanTween.alpha(optionsMenu, 0f, timeAlpha).setOnComplete(() => { optionsMenu.SetActive(false); });
        LeanTween.alpha(optionsMenu, 0f, timeAlpha).setOnComplete(() => { optionsMenuInGame.SetActive(false); });
    }
    public void Restart()
    {
        GameManager.Instance.ReempezarPartida();
        LeanTween.alpha(optionsMenu, 0f, timeAlpha).setOnComplete(() => { optionsMenu.SetActive(false); });
    }

    // Método para botón 'Salir'
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();

        // Para que también funcione si estás dentro del Editor de Unity
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
