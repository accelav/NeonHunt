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
    [SerializeField] TextMeshProUGUI recordText;
    [SerializeField] GameObject dieScreen;
    [SerializeField] GameObject winScreen;

    float timeRemaining = 420f;
    bool estaPausada;

    private void Start()
    {
        optionsMenuInGame.SetActive(false);
        GameManager.Instance.estaContando = true;
        winScreen.SetActive(false);
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

        if (GameManager.Instance.hasGanado == false)
        {
            winScreen.SetActive(false);
        }
        else
        {
            winScreen.SetActive(true);
        }

        recordText.text = "Tu record es de " + GameManager.Instance.bestScore.ToString() + " puntos";
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
        SoundsBehaviour.instance.PlayButtonSound();
        GameManager.Instance.EmpezarPartida();
    }

    public void OpenOptionsInGame()
    {

        optionsMenuInGame.SetActive(true);
        //LeanTween.alpha(optionsMenuInGame, 1f, timeAlpha);
    }
    public void ToggleOptions()
    {
        SoundsBehaviour.instance.PlayButtonSoundTwo();
        GameManager.Instance.TogglePauseGame();
    }
    public void BackButton()
    {
        SoundsBehaviour.instance.PlayButtonSoundTwo();
        if (GameManager.Instance.partidaPausada)
            GameManager.Instance.TogglePauseGame();
    }

    public void Restart()
    {
        SoundsBehaviour.instance.PlayButtonSoundTres();
        GameManager.Instance.ReempezarPartida();
    }
    public void VolverMainMenu()
    {
        SoundsBehaviour.instance.PlayButtonSoundTres();
        GameManager.Instance.VolverAlMenu();    
    }
    public void QuitGame()
    {
        SoundsBehaviour.instance.PlayButtonSoundTres();
        Debug.Log("Saliendo del juego...");
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }
}
