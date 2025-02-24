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
    [SerializeField]
    GameObject dieScreen;
    float timeRemaining = 420f;
    bool estaPausada;

    private void Start()
    {
        optionsMenuInGame.SetActive(false);
        GameManager.Instance.estaContando = true;
        
    }

    private void Update()
    {
        if (GameManager.Instance.esperarParDisparar)
        {
            // Prioridad 1: Recargando
            warningText.text = "Recargando";
        }
        else if (GameManager.Instance.estaDetectando)
        {
            // Prioridad 2: Detectado
            warningText.text = "Detectado";
            Debug.Log("Player Detectado");
        }
        else
        {
            // Prioridad 3: Sigilo (por descarte)
            warningText.text = "Sigilo";
        }

        if (GameManager.Instance.estaContando)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }

        }

        if (GameManager.Instance.estaMuerto)
        {
            dieScreen.SetActive(true);
        }
        else
        {
            dieScreen.SetActive(false);
        }

        pointsText.text = GameManager.Instance.puntosTotales.ToString();

        estaPausada = GameManager.Instance.partidaPausada;
        if (!estaPausada)
        {
            optionsMenuInGame.SetActive(false);
        }
        else
        {
            optionsMenuInGame.SetActive(true);
            LeanTween.alpha(optionsMenuInGame, 1f, timeAlpha);
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
        
    }

    public void OpenOptionsInGame()
    {
        
        optionsMenuInGame.SetActive(true);
        LeanTween.alpha(optionsMenuInGame, 1f, timeAlpha);

    }

    public void BackButton()
    {

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
