using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // -- NUEVO: Variable para guardar el récord --
    public int bestScore = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // -- NUEVO: Cargamos el récord al iniciar --
            bestScore = PlayerPrefs.GetInt("bestScore", 0);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        startButton = false;
        timer = 420f;
    }

    private void Update()
    {
        enemigosActuales = enemigosTotales;

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            TogglePauseGame();
        }

        if (enemigosActuales <= 0 && !hasGanado)
        {
            PartidaGanada();
            hasGanado = true; // Para evitar llamar a PartidaGanada repetidamente
        }
        else if (enemigosActuales > 0)
        {
            timer -= Time.deltaTime;
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
        partidaPausada = false; // Reiniciamos el estado de pausa
        startButton = true;
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
        ResumeGame(); // Reanuda el juego antes de recargar la escena
        restarting = true;
        estaMuerto = false;
        startButton = false;
        timer = 0f;
        enemigosTotales = 0;
        ResetPuntos();
        SceneManager.LoadScene("GameScene");
    }

    public void VolverAlMenu()
    {
        ResumeGame(); // Reanuda el juego antes de recargar la escena
        restarting = true;
        estaMuerto = false;
        startButton = false;
        ResetPuntos();
        enemigosTotales = 0;
        SceneManager.LoadScene("MainMenu");
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

        // -- NUEVO: Actualizamos el récord si se supera
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

    public void EnemiesCounter(int enemigos)
    {
        enemigosTotales += enemigos;
    }
}
