using UnityEngine;
using UnityEngine.SceneManagement; // Para cargar escenas
#if UNITY_EDITOR
using UnityEditor;                // Para salir desde el editor
#endif
using TMPro;

public class ScreensController : MonoBehaviour
{
    public GameObject optionsMenuInGame;
    public float timeAlpha;
    [SerializeField]
    TextMeshProUGUI warningText;
    [SerializeField]
    TextMeshProUGUI timerText;
    [SerializeField]
    TextMeshProUGUI pointsText;
    float timeRemaining = 420f;

    private void Start()
    {
        optionsMenuInGame.SetActive(false);
        GameManager.Instance.estaContando = true;
    }

    private void Update()
    {
        if (GameManager.Instance.estaDetectando)
        {
            warningText.text = "Detectado";
        }
        if (GameManager.Instance.estaDetectando == false)
        {
            warningText.text = "Sigilo";
        }
        if (GameManager.Instance.esperarParDisparar)
        {
            warningText.text = "Recargando";
        }

        if (GameManager.Instance.estaContando)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }

        }
    }

    void DisplayTime(float timeToDisplay)
    {
        // Se puede ajustar sumando 1 segundo para mostrar de forma intuitiva el tiempo
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void PlayGame()
    {

        GameManager.Instance.EmpezarPartida();
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
        LeanTween.alpha(optionsMenuInGame, 0f, timeAlpha).setOnComplete(() => { optionsMenuInGame.SetActive(false); });
    }
    public void Restart()
    {
        GameManager.Instance.ReempezarPartida();
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
