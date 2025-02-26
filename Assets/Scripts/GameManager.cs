using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool partidaEmpezada = false;
    public bool partidaPausada = false;
    public bool configurando = false;
    public bool restarting = false;
    public bool estaContando = false;
    public bool startButton = false;
    public bool estaDetectando = false;
    public bool estaMuerto = false;
    public bool hasGanado = false;

    public float timer;
    public int puntosTotales;
    public int puntosIniciales;
    public int enemigosTotales;
    public int enemigosActuales;
    public bool esperarParDisparar = false;
    StarterAssetsInputs StarterAssetsInputs;
    // -- NUEVO: Variable para guardar el récord --
    public int bestScore = 0;

    // Bandera para saber si ya se han contado todos los enemigos de la escena actual.
    private bool enemigosInicializados = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            bestScore = PlayerPrefs.GetInt("bestScore", 0);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        startButton = false;
        timer = 420f;
        partidaEmpezada = false;
        StarterAssetsInputs = FindAnyObjectByType<StarterAssetsInputs>();
    }

    private void OnEnable()
    {
        // Se suscribe al evento para saber cuándo se ha cargado una escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Se llama cada vez que se carga una nueva escena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reinicia el contador de enemigos y la bandera
        enemigosTotales = 0;
        enemigosInicializados = false;
        // Espera un frame para que los Start de los enemigos se ejecuten y llamen a EnemiesCounter
        StartCoroutine(DelayedInitialization());
    }

    private IEnumerator DelayedInitialization()
    {
        yield return null;
        enemigosInicializados = true;
    }

    private void Update()
    {
        enemigosActuales = enemigosTotales;

        // Solo evaluamos la victoria una vez que se han contado los enemigos
        if (enemigosInicializados && enemigosActuales <= 0 && !hasGanado && partidaEmpezada)
        {
            PartidaGanada();
            hasGanado = true; // Evita llamadas repetidas
        }
        else if (enemigosActuales > 0 && partidaEmpezada)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                estaMuerto = true;
                timer = 420f;
            }
        }

        if (!partidaEmpezada)
        {
            UnlockCursor();
            estaContando = false;
        }
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void EmpezarPartida()
    {
        partidaEmpezada = true;
        estaContando = true;
        partidaPausada = false; // Reiniciamos el estado de pausa
        startButton = true;
        hasGanado = false;
        estaMuerto = false;
        TimerOn();
        Time.timeScale = 1f;
        LockCursor();
    }

    public void TogglePauseGame()
    {
        if (partidaPausada)
            ResumeGame();
        else
            PauseGame();
    }

    private void PauseGame()
    {
        partidaPausada = true;
        TimerOff();
        Time.timeScale = 0f;
        UnlockCursor();
    }

    private void ResumeGame()
    {
        partidaPausada = false;
        TimerOn();
        Time.timeScale = 1f;
        LockCursor();
    }

    public void JugadorMuerto()
    {
        estaMuerto = true;
        UnlockCursor();
    }

    public void PartidaGanada()
    {
        hasGanado = true;
        UnlockCursor();
        TimerOff();
        int bonus = (int)timer * 10;
        OtorgarPuntos(bonus);
    }

    public void ReempezarPartida()
    {
        restarting = true;
        // Reiniciamos variables importantes
        enemigosTotales = 0;
        estaMuerto = false;
        hasGanado = false;
        partidaEmpezada = false;
        ResetPuntos();
        // Se carga de nuevo la escena; al hacerlo, OnSceneLoaded se encargará de reinicializar el contador
        SceneManager.LoadScene("GameScene");
    }

    public void VolverAlMenu()
    {
        ResumeGame(); // Reanuda el juego antes de recargar la escena
        restarting = true;
        estaMuerto = false;
        startButton = false;
        ResetPuntos();
        UnlockCursor();
    }

    public void TimerOn()
    {
        estaContando = true;
    }

    public void TimerOff()
    {
        estaContando = false;
    }

    public void OtorgarPuntos(int puntos)
    {
        puntosTotales += puntos;
        if (puntosTotales > bestScore)
        {
            bestScore = puntosTotales;
            PlayerPrefs.SetInt("bestScore", bestScore);
            PlayerPrefs.Save();
        }
    }

    public void ResetPuntos()
    {
        puntosTotales = 0;
    }

    // Este método se llama en el Start de cada enemigo para incrementar el contador
    public void EnemiesCounter(int enemigos)
    {
        enemigosTotales += enemigos;
    }
}
