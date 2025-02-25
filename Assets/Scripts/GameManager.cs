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

    public float timer;
    public int puntosTotales;
    public int puntosIniciales;
    public int enemigosTotales;
    public bool esperarParDisparar = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        startButton = false;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            TogglePauseGame();
        }

        if (estaMuerto)
        {
            Time.timeScale = 0.0f;
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

    public void ReempezarPartida()
    {
        ResumeGame(); // Reanuda el juego antes de recargar la escena
        restarting = true;
        estaMuerto = false;
        startButton = false;
        timer = 0f;
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
        UnlockCursor();
            
        SceneManager.LoadScene("MainMenu");
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
