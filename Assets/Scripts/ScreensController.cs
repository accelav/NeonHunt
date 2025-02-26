using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;
using UnityEngine.InputSystem;


#if UNITY_EDITOR
using UnityEditor;
#endif
using TMPro;

public class ScreensController : MonoBehaviour
{
    StarterAssetsInputs starterAssetsInputs;
    public GameObject optionsMenuInGame;
    public GameObject mainMenu;
    public GameObject virtualJoystickUI;
    public float timeAlpha;
    [SerializeField] TextMeshProUGUI warningText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI timerTextWin;
    [SerializeField] TextMeshProUGUI enemiesText;
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] TextMeshProUGUI pointsTextWin;
    [SerializeField] TextMeshProUGUI recordText;
    [SerializeField] GameObject dieScreen;
    [SerializeField] GameObject winScreen;

    float timeRemaining = 420f;
    bool estaPausada;
    public bool activeUI;
    private void Start()
    {
        optionsMenuInGame.SetActive(false);
        GameManager.Instance.partidaEmpezada = false;
        winScreen.SetActive(false);
        dieScreen.SetActive(false);
        starterAssetsInputs = FindAnyObjectByType<StarterAssetsInputs>();
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
        pointsTextWin.text = GameManager.Instance.puntosTotales.ToString();
        recordText.text = GameManager.Instance.bestScore.ToString();
        float tiempoDeJuego = 420 - GameManager.Instance.timer;
        timerTextWin.text = tiempoDeJuego.ToString();

        /*estaPausada = GameManager.Instance.partidaPausada;
        if (Input)
            optionsMenuInGame.SetActive(false);
        else if(estaPausada && GameManager.Instance.partidaEmpezada)
        {
            optionsMenuInGame.SetActive(true);
            //LeanTween.alpha(optionsMenuInGame, 1f, timeAlpha);
        }*/

        winScreen.SetActive(GameManager.Instance.hasGanado);

        if (starterAssetsInputs.pause)
        {
            ToggleOptions();
            starterAssetsInputs.pause = false; // Reseteamos la variable para evitar llamadas continuas
        }


        virtualJoystickUI.SetActive(!activeUI);
 
    }

    public void ToggleUI()
    {
        activeUI = !activeUI;
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
        mainMenu.SetActive(false);
        SoundsBehaviour.instance.PlayButtonSound();
        GameManager.Instance.EmpezarPartida();
    }
    public void OpenOptionsInMenu()
    {
        SoundsBehaviour.instance.PlayButtonSound();
        optionsMenuInGame.SetActive(true);

    }

    public void ToggleOptions()
    {
        SoundsBehaviour.instance.PlayButtonSoundTwo();
        GameManager.Instance.TogglePauseGame();
        if (!GameManager.Instance.partidaPausada)
        {
            optionsMenuInGame.SetActive(false);
        }
        else
        {
            optionsMenuInGame.SetActive(true);
        }
    }

    public void OpenOptionsInGame()
    {

        optionsMenuInGame.SetActive(true);
        //LeanTween.alpha(optionsMenuInGame, 1f, timeAlpha);
    }

    public void CloseOptions()
    {
        optionsMenuInGame.SetActive(false);
    }

    public void BackButton()
    {
        SoundsBehaviour.instance.PlayButtonSoundTwo();
        if (GameManager.Instance.partidaPausada)
        {
            GameManager.Instance.TogglePauseGame();
            CloseOptions();
        }
            
        else
        {
            CloseOptions();
        }
    }


    public void Restart()
    {
        dieScreen.SetActive(false);
        winScreen.SetActive(false);
        optionsMenuInGame.SetActive(false);
        SoundsBehaviour.instance.PlayButtonSoundTres();
        
        GameManager.Instance.ReempezarPartida();
        
    }
    public void VolverMainMenu()
    {
        SoundsBehaviour.instance.PlayButtonSoundTres();
        mainMenu.SetActive(true);
        //GameManager.Instance.VolverAlMenu();    
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
