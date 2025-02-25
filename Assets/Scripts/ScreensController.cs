using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
using TMPro;

public class ScreensController : MonoBehaviour
{
    public GameObject optionsMenuInGame;
    public float timeAlpha;
    [SerializeField] TextMeshProUGUI warningText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI enemiesText;
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] GameObject dieScreen;

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
            warningText.text = "Recargando";
        else if (GameManager.Instance.estaDetectando)
        {
            warningText.text = "Detectado";
            Debug.Log("Player Detectado");
        }
        else
            warningText.text = "Sigilo";

        if (GameManager.Instance.estaContando && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }

        dieScreen.SetActive(GameManager.Instance.estaMuerto);
        enemiesText.text = GameManager.Instance.enemigosTotales.ToString() + " Enemigos";
        pointsText.text = GameManager.Instance.puntosTotales.ToString();

        estaPausada = GameManager.Instance.partidaPausada;
        if (!estaPausada)
            optionsMenuInGame.SetActive(false);
        else
        {
            optionsMenuInGame.SetActive(true);
            //LeanTween.alpha(optionsMenuInGame, 1f, timeAlpha);
        }


    }

    void DisplayTime(float timeToDisplay)
    {
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
        optionsMenuInGame.SetActive(true);
        //LeanTween.alpha(optionsMenuInGame, 1f, timeAlpha);
    }
    public void ToggleOptions()
    {
        GameManager.Instance.TogglePauseGame();
    }
    public void BackButton()
    {
        if (GameManager.Instance.partidaPausada)
            GameManager.Instance.TogglePauseGame();
    }

    public void Restart()
    {
        GameManager.Instance.ReempezarPartida();
    }
    public void VolverMainMenu()
    {

        GameManager.Instance.VolverAlMenu();    
    }
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
